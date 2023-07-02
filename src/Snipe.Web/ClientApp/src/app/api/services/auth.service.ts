/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpContext } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { RefreshTokenRequest } from '../models/refresh-token-request';
import { RefreshTokenResponse } from '../models/refresh-token-response';
import { SendEmailVerificationCodeRequest } from '../models/send-email-verification-code-request';
import { UserInfoDto } from '../models/user-info-dto';
import { UserSignInRequest } from '../models/user-sign-in-request';
import { UserSignInResponse } from '../models/user-sign-in-response';
import { UserSignUpRequest } from '../models/user-sign-up-request';
import { UserSignUpResponse } from '../models/user-sign-up-response';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiAuthSignInPost
   */
  static readonly ApiAuthSignInPostPath = '/api/auth/sign-in';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthSignInPost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignInPost$Plain$Response(params?: {
    body?: UserSignInRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<UserSignInResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthSignInPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserSignInResponse>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthSignInPost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignInPost$Plain(params?: {
    body?: UserSignInRequest
  },
  context?: HttpContext

): Observable<UserSignInResponse> {

    return this.apiAuthSignInPost$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<UserSignInResponse>) => r.body as UserSignInResponse)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthSignInPost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignInPost$Json$Response(params?: {
    body?: UserSignInRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<UserSignInResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthSignInPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserSignInResponse>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthSignInPost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignInPost$Json(params?: {
    body?: UserSignInRequest
  },
  context?: HttpContext

): Observable<UserSignInResponse> {

    return this.apiAuthSignInPost$Json$Response(params,context).pipe(
      map((r: StrictHttpResponse<UserSignInResponse>) => r.body as UserSignInResponse)
    );
  }

  /**
   * Path part for operation apiAuthSignUpPost
   */
  static readonly ApiAuthSignUpPostPath = '/api/auth/sign-up';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthSignUpPost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignUpPost$Plain$Response(params?: {
    body?: UserSignUpRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<UserSignUpResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthSignUpPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserSignUpResponse>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthSignUpPost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignUpPost$Plain(params?: {
    body?: UserSignUpRequest
  },
  context?: HttpContext

): Observable<UserSignUpResponse> {

    return this.apiAuthSignUpPost$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<UserSignUpResponse>) => r.body as UserSignUpResponse)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthSignUpPost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignUpPost$Json$Response(params?: {
    body?: UserSignUpRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<UserSignUpResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthSignUpPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserSignUpResponse>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthSignUpPost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSignUpPost$Json(params?: {
    body?: UserSignUpRequest
  },
  context?: HttpContext

): Observable<UserSignUpResponse> {

    return this.apiAuthSignUpPost$Json$Response(params,context).pipe(
      map((r: StrictHttpResponse<UserSignUpResponse>) => r.body as UserSignUpResponse)
    );
  }

  /**
   * Path part for operation apiAuthSendEmailVerificationCodePost
   */
  static readonly ApiAuthSendEmailVerificationCodePostPath = '/api/auth/send-email-verification-code';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthSendEmailVerificationCodePost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSendEmailVerificationCodePost$Plain$Response(params?: {
    body?: SendEmailVerificationCodeRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<string>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthSendEmailVerificationCodePostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<string>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthSendEmailVerificationCodePost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSendEmailVerificationCodePost$Plain(params?: {
    body?: SendEmailVerificationCodeRequest
  },
  context?: HttpContext

): Observable<string> {

    return this.apiAuthSendEmailVerificationCodePost$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<string>) => r.body as string)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthSendEmailVerificationCodePost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSendEmailVerificationCodePost$Json$Response(params?: {
    body?: SendEmailVerificationCodeRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<string>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthSendEmailVerificationCodePostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<string>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthSendEmailVerificationCodePost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthSendEmailVerificationCodePost$Json(params?: {
    body?: SendEmailVerificationCodeRequest
  },
  context?: HttpContext

): Observable<string> {

    return this.apiAuthSendEmailVerificationCodePost$Json$Response(params,context).pipe(
      map((r: StrictHttpResponse<string>) => r.body as string)
    );
  }

  /**
   * Path part for operation apiAuthRefreshTokenPost
   */
  static readonly ApiAuthRefreshTokenPostPath = '/api/auth/refresh-token';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthRefreshTokenPost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthRefreshTokenPost$Plain$Response(params?: {
    body?: RefreshTokenRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<RefreshTokenResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthRefreshTokenPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<RefreshTokenResponse>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthRefreshTokenPost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthRefreshTokenPost$Plain(params?: {
    body?: RefreshTokenRequest
  },
  context?: HttpContext

): Observable<RefreshTokenResponse> {

    return this.apiAuthRefreshTokenPost$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<RefreshTokenResponse>) => r.body as RefreshTokenResponse)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthRefreshTokenPost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthRefreshTokenPost$Json$Response(params?: {
    body?: RefreshTokenRequest
  },
  context?: HttpContext

): Observable<StrictHttpResponse<RefreshTokenResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthRefreshTokenPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<RefreshTokenResponse>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthRefreshTokenPost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAuthRefreshTokenPost$Json(params?: {
    body?: RefreshTokenRequest
  },
  context?: HttpContext

): Observable<RefreshTokenResponse> {

    return this.apiAuthRefreshTokenPost$Json$Response(params,context).pipe(
      map((r: StrictHttpResponse<RefreshTokenResponse>) => r.body as RefreshTokenResponse)
    );
  }

  /**
   * Path part for operation apiAuthUserGet
   */
  static readonly ApiAuthUserGetPath = '/api/auth/user';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthUserGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAuthUserGet$Plain$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<UserInfoDto>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthUserGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserInfoDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthUserGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAuthUserGet$Plain(params?: {
  },
  context?: HttpContext

): Observable<UserInfoDto> {

    return this.apiAuthUserGet$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<UserInfoDto>) => r.body as UserInfoDto)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAuthUserGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAuthUserGet$Json$Response(params?: {
  },
  context?: HttpContext

): Observable<StrictHttpResponse<UserInfoDto>> {

    const rb = new RequestBuilder(this.rootUrl, AuthService.ApiAuthUserGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserInfoDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiAuthUserGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAuthUserGet$Json(params?: {
  },
  context?: HttpContext

): Observable<UserInfoDto> {

    return this.apiAuthUserGet$Json$Response(params,context).pipe(
      map((r: StrictHttpResponse<UserInfoDto>) => r.body as UserInfoDto)
    );
  }

}
