import { Component, OnDestroy } from '@angular/core';
import { Select } from '@ngxs/store';
import { Observable, Subscription } from 'rxjs';
import { CoreState } from '../state/core.state';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnDestroy {

  @Select(CoreState.isAuthenticated)
  public readonly isAuthenticated$!: Observable<boolean>;

  private subscription: Subscription;

  constructor(private authService: AuthService) {
    this.subscription = this.isAuthenticated$
      .subscribe(isAuthenticated => {
        if (!isAuthenticated) {
          this.authService.login();
        }
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}