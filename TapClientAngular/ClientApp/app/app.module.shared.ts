import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { HeaderComponent } from './components/shared/header/header.component'
import { ServerListComponent } from './components/vulnerabilities/server/server-list/server-list.component'
import { VulnerabilityListComponent } from './components/vulnerabilities/vulnerability/vulnerability-list/vulnerability-list.component'

import { VulnerabilityService } from './components/vulnerabilities/shared/vulnerability.service'

import {AuthGuardService } from './components/shared/services/auth-guard.service'
import {AuthService } from './components/shared/services/auth.service'

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        HeaderComponent,
        ServerListComponent,
        VulnerabilityListComponent,
        AuthCallbackComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent, canActivate: [AuthGuardService] },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'auth-callback', component: AuthCallbackComponent },
            { path: 'vulnerabilities/server', component: ServerListComponent, canActivate: [AuthGuardService] },
            { path: 'vulnerabilities/server/details/:id', component: VulnerabilityListComponent, canActivate: [AuthGuardService] },
            { path: 'vulnerabilities/details', component: VulnerabilityListComponent, canActivate: [AuthGuardService] },
            { path: '**', redirectTo: 'home' }
        ]),
        BsDropdownModule.forRoot(),
        PaginationModule.forRoot()
    ],
    providers: [ VulnerabilityService, AuthGuardService, AuthService ],
    exports:[RouterModule]

})
export class AppModuleShared {
}
