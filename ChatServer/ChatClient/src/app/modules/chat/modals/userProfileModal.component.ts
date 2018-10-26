import { UserProfile } from 'src/app/shared/models/UserProfile.model';
import { APIService } from './../../../core/http/api.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, Input, EventEmitter, Output } from "@angular/core";

@Component({
    templateUrl:"./userProfileModal.component.html",
    styleUrls:["./userProfileModal.component.css"]
})
export class UserProfileModal{

    constructor(public activeModal:NgbActiveModal ){}

    @Input('userProfile')
    public userProfile:UserProfile;

    @Output()
    uploadAvatar = new EventEmitter<File>();

    uploadAvatarHandler(avatar:File){
        this.uploadAvatar.emit(avatar);
    }

}