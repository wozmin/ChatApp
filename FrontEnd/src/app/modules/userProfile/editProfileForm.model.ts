import { FormControl, FormGroup, Validators } from "@angular/forms";

export class EditProfileFormControl extends FormControl{
    label:string;
    modelProperty:string;
    type:string;

    constructor(label:string,property:string,type:string,value:any,validator:any){
        super(value,validator);
        this.label = label;
        this.modelProperty = property;
        this.type = type;
    }

    getValidationMessages(){
        let messages:string[] = [];
        if(this.errors){
            for(let errorName in this.errors){
                switch(errorName){
                    case "required":
                        messages.push(`You must enter a ${this.label}`);
                        break;
                    case "pattern":
                        messages.push(`The ${this.label} contains illegal characters`);
                        break;

                }
            }
        }
        return messages;
    }
}

export class EditProfileFormGroup extends FormGroup{
    constructor(){
        super({
            userName:new EditProfileFormControl("UserName","userName","text","",Validators.compose([
                Validators.required,
                Validators.pattern("[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*")
            ])),
            email:new EditProfileFormControl("Email","email","email","",Validators.compose([
                Validators.required,
                Validators.pattern("^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(?:aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$")
            ])),
            age:new EditProfileFormControl("Age","age","number","",Validators.compose([
                Validators.required,
                Validators.pattern("^[1-9][0-9]?$")
            ])),
            address:new EditProfileFormControl("Address","address","text","",Validators.compose([
                Validators.required,
            ]))

        })
    }
    get signInControls():EditProfileFormControl[]{
        return Object.keys(this.controls)
                .map(k=>this.controls[k] as EditProfileFormControl);
    }
}