import { Injectable, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthService } from './auth.service'
import { Observable } from "rxjs/Observable";

@Injectable()
export class AuthGuardService implements CanActivate {
  private isBrowser:boolean;

  constructor(private authService: AuthService,@Inject(PLATFORM_ID) platformId: Object) { 
      this.isBrowser=isPlatformBrowser(platformId);
  }
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
      if(!this.isBrowser){
        return false;
      }
      if(this.authService.isLoggedIn()) {
        return true;
      }
      sessionStorage.setItem('redirectURL',state.url);
      this.authService.redirectUrl=state.url
      this.authService.startAuthentication();
      return false;
  }
}