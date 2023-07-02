import { Component, ElementRef, OnDestroy, OnInit, QueryList, ViewChildren } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Select, Store } from '@ngxs/store';
import { Observable, Subscription } from 'rxjs';
import { CoreState } from '../state/core.state';
import { AuthService } from 'src/app/api/services/auth.service';
import { UserSignInRequest } from 'src/app/api/models';
import { TokenStoreService } from '../services/token-store.service';
import { UserSignedIn } from '../state/core.actions';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html'
})
export class SignInComponent implements OnInit, OnDestroy {

  @ViewChildren('autofocus') autofocusElements!: QueryList<ElementRef>;

  @Select(CoreState.isAuthenticated)
  public readonly isAuthenticated$!: Observable<boolean>;

  error: string = '';
  loading: boolean = false;

  form = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  private subscriptions: Subscription[] = [];

  constructor(
    private readonly store: Store,
    private readonly authService: AuthService,
    private readonly tokenStore: TokenStoreService,
    private readonly router: Router) { }

  ngOnInit(): void {
    if (this.autofocusElements && this.autofocusElements.length > 0) {
      this.autofocusElements.first.nativeElement.focus();
    }
  }

  submit() {
    this.loading = true;

    const subscription = this.authService
      .apiAuthSignInPost$Json({ body: <UserSignInRequest>this.form.value })
      .subscribe(result => {
        if (result.success && result.accessToken && result.refreshToken) {
          this.tokenStore.setAccessToken(result.accessToken);
          this.tokenStore.setRefreshToken(result.refreshToken);
        }
        this.store.dispatch(new UserSignedIn("",""));
        this.router.navigate(['/']);
      });

    this.subscriptions.push(subscription);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
}