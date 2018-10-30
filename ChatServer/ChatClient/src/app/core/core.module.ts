import { RouterModule } from '@angular/router';
import { AuthService } from './authentication/auth.service';
import { APIService } from './http/api.service';
import { NgModule } from "@angular/core";
import { AuthGuard } from './guards/auth.guard';
import { JwtHttpInterceptor } from './interceptors/jwt.interceptor';
import { HubService } from './services/hub.service';
import { AppHeaderComponent } from './header/app-header.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faUser, faUsers, faAddressBook, faArrowLeft, faSignOutAlt, faComment } from '@fortawesome/free-solid-svg-icons';
import { BrowserModule } from '@angular/platform-browser';

library.add(faUser,faUsers,faAddressBook,faArrowLeft,faSignOutAlt,faComment);
@NgModule({
    imports:[FontAwesomeModule,BrowserModule,RouterModule],
    declarations:[AppHeaderComponent],
    providers:[APIService,AuthService,HubService,AuthGuard,JwtHttpInterceptor],
    exports:[AppHeaderComponent]
})
export class CoreModule{}