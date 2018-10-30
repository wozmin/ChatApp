import { HttpModule } from '@angular/http';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthModule } from './modules/auth/auth.module';
import { AppRoutingModule } from './../app-routing.module';
import { CoreModule } from './core/core.module';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module';
import { ChatModule } from './modules/chat/chat.module';
import { JwtHttpInterceptor } from './core/interceptors/jwt.interceptor';
import { NotifierModule, NotifierService} from 'angular-notifier';
import { UserProfileModule } from './modules/userProfile/userProfil.module';

@NgModule({
    imports:[BrowserModule,FormsModule,SharedModule,AppRoutingModule,ChatModule,
            AuthModule,CoreModule,HttpClientModule,HttpModule,NotifierModule,UserProfileModule],
    declarations:[AppComponent],
    providers:[{provide:HTTP_INTERCEPTORS,useClass:JwtHttpInterceptor,multi:true},NotifierService],
    bootstrap:[AppComponent]
})
export class AppModule{}