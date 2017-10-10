import { Component, OnInit, PLATFORM_ID, Inject  } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { AuthService } from '../shared/services/auth.service'

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html'
})
export class AuthCallbackComponent implements OnInit {

  private isBrowser:boolean;

  constructor(private authService: AuthService,@Inject(PLATFORM_ID) platformId: Object) { 
    this.isBrowser=isPlatformBrowser(platformId);
  }

  ngOnInit() {
    if (this.isBrowser){
      this.authService.completeAuthentication();
    }

  }
}