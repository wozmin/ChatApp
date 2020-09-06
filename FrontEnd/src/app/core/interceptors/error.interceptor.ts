import { catchError } from "rxjs/operators";
import { AuthService } from "../authentication/auth.service";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { Router } from "@angular/router";
import { NotifierService } from "angular-notifier";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private authService: AuthService,
        private router:Router,
        private notifierService:NotifierService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                
                this.authService.logout();
                this.router.navigateByUrl("/login");
            }
            if(err.status === 500){
                this.notifierService.notify('error','Something went wrong on the server');
            }
            
            const error = err.error.message || err.statusText;
            return throwError(error);
        }))
    }
}