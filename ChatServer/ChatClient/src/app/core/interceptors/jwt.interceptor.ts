import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';                                                        
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../authentication/auth.service';

@Injectable()
export class JwtHttpInterceptor implements HttpInterceptor {
  constructor(private authService:AuthService,private router:Router) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
      let clone: HttpRequest<any>;
      if (token) {
        clone = request.clone({
          setHeaders: {
            Accept: `application/json`,
            'Content-Type': `application/json`,
            Authorization: `Bearer ${token}`
          }
        });
      } else {
        clone = request.clone({
          setHeaders: {
            Accept: `application/json`,
            'Content-Type': `application/json`
          }
        });
      }
      return next.handle(clone).pipe(catchError(error=>{
          if(error.status === 401){
              this.authService.logout();
              this.router.navigateByUrl('/login');
          }

          return throwError(error);
      }));
  }
}
