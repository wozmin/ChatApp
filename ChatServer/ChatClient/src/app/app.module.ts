import { HttpModule } from '@angular/http';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginModule } from './modules/login/login.module';
import { AppRoutingModule } from './../app-routing.module';
import { CoreModule } from './core/core.module';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module';
import { ChatModule } from './modules/chat/chat.module';
import { JwtHttpInterceptor } from './core/interceptors/jwt.interceptor';
import { UserListModal } from './modules/chat/modals/userListModal.component';

@NgModule({
    imports:[BrowserModule,FormsModule,SharedModule,AppRoutingModule,ChatModule,
            LoginModule,CoreModule,HttpClientModule,HttpModule],
    declarations:[AppComponent],
    providers:[{provide:HTTP_INTERCEPTORS,useClass:JwtHttpInterceptor,multi:true}],
    bootstrap:[AppComponent]
})
export class AppModule{}