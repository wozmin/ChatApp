import { UserProfile } from './../../../shared/models/UserProfile.model';
import { Component, Input } from "@angular/core";

@Component({
    templateUrl:'./editUserModal.component.html',
    styleUrls:['./editUserModal.component.css']
})
export class EditUserModal{

    @Input()
    userProfile:UserProfile;

    
}