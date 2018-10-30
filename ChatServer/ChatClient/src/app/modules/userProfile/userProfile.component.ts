import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { APIService } from './../../core/http/api.service';
import { Component } from "@angular/core";
import { UserProfile } from "src/app/shared/models/UserProfile.model";
import { Router, ActivatedRoute } from '@angular/router';
import { newChatModal } from '../chat/modals/newChatModal.component';
import { NotifierService } from 'angular-notifier';
import { AuthService } from 'src/app/core/authentication/auth.service';



@Component({
    templateUrl:'./userProfile.component.html',
    styleUrls:['./userProfile.component.css']
})
export class UserProfileComponent{

    isCurrentUser:boolean;
    userProfile:UserProfile;
    userId:string;

    constructor(
        private apiService:APIService,
        private route:ActivatedRoute,
        private modalService:NgbModal,
        private router:Router,
        private notifierService:NotifierService,
        private authService:AuthService
        ){
        this.userId = this.route.snapshot.params['id'];
        this.isCurrentUser = this.userId === this.authService.getUserId();
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

    changeAvatar(avatar:File){
        this.apiService.uploadAvatar(avatar);
    }

    deleteAvatar(){
        this.apiService.deleteAvatar().subscribe(res=>{
            this.notifierService.notify('success','Avatar was deleted successfully');
        })
    }
}