import { EditUserModel } from './../../shared/models/editUser.model';
import { APIService } from 'src/app/core/http/api.service';
import { Component } from "@angular/core";
import { UserProfile } from 'src/app/shared/models/UserProfile.model';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotifierService } from 'angular-notifier';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { newChatModal } from '../chat/modals/newChatModal.component';
import { EditProfileFormGroup } from './editProfileForm.model';
import { NgForm } from '@angular/forms';

@Component({
    templateUrl:'./editUserProfile.component.html',
    styleUrls:["./editUserProfile.component.css"]
})
export class EditUserProfileComponent{

    userProfile:UserProfile;
    userId:string;
    formGroup:EditProfileFormGroup = new EditProfileFormGroup();
    formSubmitted:boolean = false;

    constructor(
        private apiService:APIService,
        private route:ActivatedRoute,
        private modalService:NgbModal,
        private router:Router,
        private notifierService:NotifierService,
        ){
        this.userId = this.route.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.apiService.getUserProfile(this.userId).subscribe(profile=>{
            this.userProfile = profile;
        })
    }

    openNewChatModal(){
        const modalRef = this.modalService.open(newChatModal,{
            windowClass:'animated fadeInDown',
            size:'sm',
            centered:true
        }).result.then(chatName=>{
            this.apiService.createChat(chatName).subscribe(data=>{
                this.router.navigateByUrl('');
            });
        });
    }

    submitForm(form:NgForm){
        this.formSubmitted = true;
        if(form.valid){
            this.apiService.updateUser(new EditUserModel(
                this.userProfile.email,
                this.userProfile.userName,
                this.userProfile.age,
                this.userProfile.address
            ));
            setTimeout(()=>{
                this.router.navigateByUrl(`/user/${this.userId}/profile`);
            },300)
        }
    }

    cancel(){
        this.router.navigateByUrl(`/user/${this.userId}/profile`);
    }

}