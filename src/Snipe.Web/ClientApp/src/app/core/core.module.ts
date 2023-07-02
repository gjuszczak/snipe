import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { MenuModule } from 'primeng/menu';
import { PasswordModule } from 'primeng/password';
import { ProgressBarModule } from 'primeng/progressbar';
import { RippleModule } from 'primeng/ripple';
import { SidebarModule } from 'primeng/sidebar';
import { SkeletonModule } from 'primeng/skeleton';
import { ToastModule } from 'primeng/toast';

import { CoreRoutingModule } from './core-routing.module';

import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { LogoutComponent } from './logout/logout.component';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { BrandComponent } from './brand/brand.component';
import { LayoutComponent } from './layout/layout.component';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  imports: [
    CommonModule,
    CoreRoutingModule,
    ReactiveFormsModule,

    // PrimeNg
    ButtonModule,
    CardModule,
    InputTextModule,
    MenuModule,
    PasswordModule,
    ProgressBarModule,
    RippleModule,
    SidebarModule,
    SkeletonModule,
    ToastModule,
  ],
  declarations: [
    BrandComponent,
    FooterComponent,
    HeaderComponent,
    HomeComponent,
    LayoutComponent,
    SignInComponent,
    LogoutComponent,
    PrivacyPolicyComponent,
    SideNavComponent,
  ],
  exports: [
    LayoutComponent,
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule?: CoreModule) {
    if (parentModule) {
      throw new Error(`CoreModule has already been loaded. Import Core modules in the AppModule only.`);
    }
  }
}
