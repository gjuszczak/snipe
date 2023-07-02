import { Inject, Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, catchError, of, switchMap, tap, throwError } from 'rxjs';
import { TokenStoreService } from './token-store.service';
import { AuthService } from 'src/app/api/services';

@Injectable()
export class AuthRequestInterceptor implements HttpInterceptor {

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private tokenStore: TokenStoreService,
    private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const isApiRequest = request.url.startsWith(this.baseUrl);
    return isApiRequest
      ? this.handleApiRequest(request, next)
      : next.handle(request);
  }

  private handleApiRequest(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return this.handleRequestWithToken(request, next).pipe(
      catchError((error) => {
        if (error.status === 401) {
          return this.handleUnauthorizedError(request, next);
        }
        return throwError(() => error);
      })
    );
  }

  private handleRequestWithToken(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken = this.tokenStore.getAccessToken();
    if (accessToken) {
      return this.sendRequestWithToken(request, next, accessToken);
    }

    const refreshToken = this.tokenStore.getRefreshToken();
    if (!refreshToken) {
      return this.throwRefreshTokenNotAvailable();
    }

    return this.exchangeRefreshToken(refreshToken).pipe(
      switchMap((newAccessToken: string) => {
        return this.sendRequestWithToken(request, next, newAccessToken);
      })
    );
  }

  private sendRequestWithToken(
    request: HttpRequest<any>,
    next: HttpHandler,
    accessToken: string
  ): Observable<HttpEvent<any>> {
    const requestWithToken = request.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`
      }
    });

    return next.handle(requestWithToken);
  }

  private handleUnauthorizedError(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const refreshToken = this.tokenStore.getRefreshToken();
    if (!refreshToken) {
      return this.throwRefreshTokenNotAvailable();
    }

    return this.exchangeRefreshToken(refreshToken).pipe(
      switchMap((newAccessToken: string) => {
        return this.sendRequestWithToken(request, next, newAccessToken);
      }),
      catchError((refreshError) => {
        // Handle the refresh token exchange error here (e.g., logout the user)
        // and re-throw the original error
        // ...

        return throwError(() => refreshError);
      })
    );
  }

  private exchangeRefreshToken(refreshToken: string) {
    return this.authService.apiAuthRefreshTokenPost$Json({ body: { refreshToken } }).pipe(
      tap(result => {
        if (result.success && result.accessToken && result.refreshToken) {
          this.tokenStore.setAccessToken(result.accessToken);
          this.tokenStore.setRefreshToken(result.refreshToken);
        }
        else {
          this.tokenStore.clearAccessToken();
          this.tokenStore.clearRefreshToken();
        }
      }),
      switchMap(result => {
        if (result.success && result.accessToken) {
          return of(result.accessToken);
        }
        return this.throwInvalidRefreshToken();
      })
    )
  }

  private throwRefreshTokenNotAvailable() {
    return throwError(() => new Error('Refresh token not available'));
  }

  private throwInvalidRefreshToken() {
    return throwError(() => new Error('Invalid refresh token'));
  }
}
