import { Router, CanActivate } from '@angular/router';
import { Injectable } from "@angular/core";
import { AuthService } from '../authentication/auth.service';


@Injectable()
export class AuthGuard implements CanActivate{

    constructor(private router:Router,private auth:AuthService){}

    canActivate():boolean{
        if(this.auth.isTokenExpired()) {
            this.router.navigateByUrl('login');
            return false;
        }
        return true;
    }
}