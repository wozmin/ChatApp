import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import * as jwt_decode from 'jwt-decode';

export const TOKEN_NAME: string = 'access-token';

@Injectable()
export class AuthService {

  private url: string = 'http://localhost:53809/api/account';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  private userId:string;
  private userName:string;

  constructor(private http: Http) { }  
    
    getUserId():string{
        return localStorage.getItem('userId');
    }

    setUserId(userId:string){
        localStorage.setItem('userId',userId);
    }

    getUserName():string{
      return localStorage.getItem('userName');
    }

    setUserName(userName:string){
        localStorage.setItem('userName',userName);
    }

  getToken(): string {
    return localStorage.getItem(TOKEN_NAME);
  }

  setToken(token: string): void {
    localStorage.setItem(TOKEN_NAME, token);
  }

  logout(){
      localStorage.removeItem(TOKEN_NAME);
      localStorage.removeItem('userName');
      localStorage.removeItem('userId');
  }

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);

    if (decoded.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean {
    if(!token) token = this.getToken();
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

  login(user) {
    return this.http
      .post(`${this.url}/login`, user, { headers: this.headers })
      .toPromise()
      .then(res => res.json())
      .then(res=>{
          this.setToken(res.token);
          this.setUserId(res.userId);
          this.setUserName(res.userName);
        });
  }

  register(user){
      return this.http.post(`${this.url}/register`,user,{headers:this.headers})
      .toPromise()
      .then(res => res.json())
      .then(res=>{
          this.setToken(res.token);
          this.setUserId(res.userId);
          this.setUserName(res.userName);
        });
  }
}