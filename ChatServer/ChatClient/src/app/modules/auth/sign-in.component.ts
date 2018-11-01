import { HubService } from './../../core/services/hub.service';
import { NotifierService } from 'angular-notifier';
import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "src/app/core/authentication/auth.service";

@Component({
    templateUrl:"./sign-in.component.html",
    styleUrls:["./sign-in.component.css"]
})
export class SignInComponent{
    username:string;
    password:string;
    error:string;
    formSubmitted:boolean;

    constructor(
        private authService:AuthService,
        private router:Router,
        private notifierService:NotifierService,
        private hubService:HubService
        ){}

    login(form:NgForm){
        this.formSubmitted = true;
        if(form.valid){
            this.authService.login({
                userName:this.username,
                password:this.password
            }).then(res=>{
                this.hubService.connect();
            }).catch(
                error=>this.error = error.text());
            setTimeout(()=>{   
                if(!this.authService.isTokenExpired()){
                    this.notifierService.notify('success','You have been authenticated successfully');
                    this.router.navigateByUrl("/");
                    this.formSubmitted = false;
                    form.reset();
                }
            },500);
        }
        
    }
}