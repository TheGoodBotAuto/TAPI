import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UserManager, UserManagerSettings, User } from 'oidc-client';

@Injectable()
export class AuthService {
  private manager: UserManager = new UserManager(getClientSettings());
  private user: User;
  redirectUrl: string;

  constructor(private router: Router) {
    this.manager.getUser().then(user => {
      this.user = user;
    });
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
      console.log(this.redirectUrl + ' stuff '+ sessionStorage.getItem('redirectURL'));
      this.router.navigate([sessionStorage.getItem('redirectURL')]);
    });
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'http://localhost:5000/',
    client_id: 'angular_spa',
    redirect_uri: 'http://localhost:5002/auth-callback',
    post_logout_redirect_uri: 'http://localhost:5002/',
    response_type: "id_token token",
    scope: "openid profile api1",
    filterProtocolClaims: true,
    loadUserInfo: true
  };
}