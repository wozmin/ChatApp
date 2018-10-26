import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "src/app/core/authentication/auth.service";

@Component({
    templateUrl:"./login.component.html",
    styleUrls:["./login.component.css"]
})
export class LoginComponent{
    username:string;
    password:string;

    formSubmitted:boolean;

    constructor(private authService:AuthService,private router:Router){}

    login(form:NgForm){
        this.formSubmitted = true;
        if(form.valid){
            this.authService.login({userName:this.username,password:this.password});
            setTimeout(()=>{   
                if(!this.authService.isTokenExpired()){
                    this.router.navigateByUrl("/");
                    this.formSubmitted = false;
                    form.reset();
                }
            },200);
        }
        
    }
}