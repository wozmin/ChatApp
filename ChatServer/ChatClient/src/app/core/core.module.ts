import { AuthService } from './authentication/auth.service';
import { APIService } from './http/api.service';
import { NgModule } from "@angular/core";
import { AuthGuard } from './guards/auth.guard';
import { JwtHttpInterceptor } from './interceptors/jwt.interceptor';
import { HubService } from './services/hub.service';

@NgModule({
    providers:[APIService,AuthService,HubService,AuthGuard,JwtHttpInterceptor]
})
export class CoreModule{}