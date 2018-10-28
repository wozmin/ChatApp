import { Component } from "@angular/core";
import { SignInFormGroup } from "./form.model";
import { NgForm } from "@angular/forms";
import { RegisterModel } from "src/app/shared/models/register.model";
import { AuthService } from "src/app/core/authentication/auth.service";
import { NotifierService } from "angular-notifier";
import { Router } from "@angular/router";

@Component({
    templateUrl:"./sign-up.component.html",
    styleUrls:["./auth.component.css"]
})
export class SignUpComponent{
    
    constructor(
        private authService:AuthService,
        private notifierService:NotifierService,
        private router:Router
    ){}

    formGroup:SignInFormGroup = new SignInFormGroup();
    formSubmitted:boolean = false;
    registerModel:RegisterModel;

    ngOnInit(): void {
        this.registerModel = new RegisterModel();
    }

    submitForm(form:NgForm){
        this.formSubmitted = true;
        if(form.valid){
            this.authService.register(this.registerModel)
            .catch(
                error=>console.log(error));
            setTimeout(()=>{   
                if(!this.authService.isTokenExpired()){
                    this.notifierService.notify('success','You have been authenticated successfully');
                    this.router.navigateByUrl("/");
                    this.formSubmitted = false;
                    form.reset();
                }
            },200);
        }
    }
}