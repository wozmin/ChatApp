import { HubService } from './../services/hub.service';
import { APIService } from './../http/api.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { Component, Input, Output, OnInit, EventEmitter } from "@angular/core";
import { Router } from '@angular/router';
import { UserListModal } from 'src/app/modules/chat/modals/userListModal.component';

@Component({
    selector:"app-header",
    templateUrl:"./app-header.component.html",
    styleUrls:["./app-header.component.css"]
})
export class AppHeaderComponent{

   constructor(
       private authService:AuthService,
       private router:Router,
       private modalService:NgbModal,
       private apiService:APIService,
       private hubService:HubService
       ){}
   
   isMenuVisible:boolean = false;
   userId:string;
   
   ngOnInit(): void {
       this.userId = this.authService.getUserId();
   }

   @Input('chat-name')
   chatName:string;

   @Input()
   showBackBtn:boolean = false;

    @Output('createChat')
    createChat = new EventEmitter();

    @Output()
    closeChat = new  EventEmitter();

    @Output('openChatDetails')
    openChatDetails = new EventEmitter();

    
    openChatDetailsHandler(){
        this.openChatDetails.emit();
    }

    openUsersModal(){
        const modalRef = this.modalService.open(UserListModal,{
            windowClass:'animated slideInDown'
        })
    }

    closeChatHandler(){
        this.closeChat.emit();
    }


    toggleMenu(){
        this.isMenuVisible = !this.isMenuVisible;
    }

    createChatHandler(){
        this.createChat.emit();
    }

    logout(){
        this.authService.logout();
        this.hubService.disconect();
        this.router.navigateByUrl("/login");
    }

}