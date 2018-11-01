import { HubService } from 'src/app/core/services/hub.service';
import { Component } from "@angular/core";
import { SignInFormGroup } from "./form.model";
import { NgForm } from "@angular/forms";
import { RegisterModel } from "src/app/shared/models/register.model";
import { AuthService } from "src/app/core/authentication/auth.service";
import { NotifierService } from "angular-notifier";
import { Router } from "@angular/router";

@Component({
    templateUrl:"./sign-up.component.html",
    styleUrls:["./sign-up.component.css"]
})
export class SignUpComponent{
    
    constructor(
        private authService:AuthService,
        private notifierService:NotifierService,
        private router:Router,
        private hubService:HubService
    ){}

    formGroup:SignInFormGroup = new SignInFormGroup();
    formSubmitted:boolean = false;
    registerModel:RegisterModel;
    errors:string[];

    ngOnInit(): void {
        this.registerModel = new RegisterModel();
        this.errors = [];
    }

    submitForm(form:NgForm){
        this.formSubmitted = true;
        if(form.valid){
            this.authService.register(this.registerModel).then(res=>{
                this.hubService.connect();
            })
            .catch(error=>{
                if(error.status === 400){
                    let validationErrorDictionary = JSON.parse(error.text());
                    for(var fieldName in validationErrorDictionary){
                        if(this.formGroup.controls[fieldName]){
                            this.formGroup.controls[fieldName].setErrors({invalid:true});
                        }
                        else{
                            this.errors.push(validationErrorDictionary[fieldName]);
                        }
                    }
                }
            });
            setTimeout(()=>{   
                if(!this.authService.isTokenExpired() && !this.errors){
                    this.notifierService.notify('success','You have been authenticated successfully');
                    this.router.navigateByUrl("/");
                    this.formSubmitted = false;
                    form.reset();
                }
            },200);
        }
    }
}