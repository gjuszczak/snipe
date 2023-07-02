import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Store } from '@ngxs/store';
import { NgcCookieConsentService } from 'ngx-cookieconsent';
import { PrimeNGConfig } from 'primeng/api';
import { ApiConfiguration } from './api/api-configuration';
import { ConfigureNavigation } from './core/state/core.actions';
import { UserRole } from './core/state/core.state';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy {

  private subscriptions: Subscription[] = [];

  constructor(
    private readonly store: Store,
    private apiConfiguration: ApiConfiguration,
    private ccService: NgcCookieConsentService,
    private primengConfig: PrimeNGConfig,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject('HOSTNAME') private hostname: string) {
  }

  ngOnInit() {
    this.configurePrimeNg();
    this.configureApi();
    this.configureNavigation();
    this.configureCookieConsent();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  configurePrimeNg() {
    this.primengConfig.ripple = true;
  }

  configureApi() {
    this.apiConfiguration.rootUrl = this.baseUrl.replace(/\/$/, "");
  }

  configureNavigation() {
    this.store.dispatch(new ConfigureNavigation([
      {
        label: 'Start',
        items: [
          { label: 'Homepage', icon: 'pi-home', route: '/', routeExact: true },
          { label: 'Privacy Policy', icon: 'pi-info-circle', route: '/privacy-policy', routeExact: true },
          { label: 'Sign In', icon: 'pi-sign-in', route: '/sign-in', routeExact: true },
        ]
      },
      {
        label: 'Admin',
        items: [
          { label: 'Event Log', icon: 'pi-directions', route: '/admin/event-log', routeExact: false, minimalRole: UserRole.Admin },
          { label: 'Backups', icon: 'pi-database', route: '/admin/backups', routeExact: true, minimalRole: UserRole.Admin },
          { label: 'Redirections', icon: 'pi-arrow-right-arrow-left', route: '/admin/redirections', routeExact: true, minimalRole: UserRole.Admin },
        ]
      },
      {
        label: 'User',
        items: [
          { label: 'Logout', icon: 'pi-sign-out', route: '/logout', routeExact: true, minimalRole: UserRole.None },
        ]
      },
    ]));
  }

  configureCookieConsent() {
    this.ccService.init({
      cookie: {
        domain: this.hostname
      },
      position: "bottom-right",
      theme: "classic",
      palette: {
        popup: {
          background: "#23272b",
          text: "#ffffff",
          link: "#ffffff"
        },
        button: {
          background: "#34A835",
          text: "#ffffff",
          border: "transparent"
        }
      },
      type: "info",
      content: {
        href: "/privacy-policy"
      }
    });
  }
}
