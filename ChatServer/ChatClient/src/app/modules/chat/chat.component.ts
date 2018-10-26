import { ChatDetailModal } from './modals/chatDetailModal.component';
import { UserProfile } from './../../shared/models/UserProfile.model';
import { UserProfileModal } from './modals/userProfileModal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from './../../core/authentication/auth.service';
import { HubService } from './../../core/services/hub.service';
import { ChatMessage } from 'src/app/shared/models/chat-message.model';
import { ChatDialog } from "src/app/shared/models/chatDialog.model";
import { APIService } from "src/app/core/http/api.service";
import { OnInit, Component, OnDestroy } from "@angular/core";
import { Subscription } from 'rxjs';
import { UserListModal } from './modals/userListModal.component';
import { newChatModal } from './modals/newChatModal.component';


@Component({
    selector:"chat",
    templateUrl:"./chat.component.html",
    styleUrls:["./chat.component.css"]
})
export class ChatComponent implements OnInit,OnDestroy{

    chatId:number;
    chatName:string;
    chatDialogs:ChatDialog[];
    chatMessages:ChatMessage[] = [];
    isChatOpened:boolean = false;
    userName:string;
    MessageNotification:Subscription;
    JoinChatNotification:Subscription;

    constructor(
        private apiService:APIService,
        private hubService:HubService,
        private authService:AuthService,
        private modalService:NgbModal){
    }

    ngOnInit(): void {
        this.apiService.getChats(this.authService.getUserId()).subscribe(data=>this.chatDialogs = data);
        this.userName = this.authService.getUserName();
        this.MessageNotification = this.hubService.message.subscribe(msg=>{
            console.log(msg);
            this.chatMessages.push(msg);
        });
        this.JoinChatNotification = this.hubService.chat.subscribe(chat=>{
            this.chatDialogs.push(chat);
        })
        this.hubService.connect();
    }

    openChat(chatInfo:any){
        this.isChatOpened = true;
        this.chatMessages = [];
        this.chatId = chatInfo.chatId;
        this.chatName = chatInfo.chatName;
        this.apiService.getChatMessages(this.chatId).subscribe(data=>this.chatMessages = data);
        this.hubService.joinChat(this.authService.getUserId(),this.chatId);
    }

    openUsersModal(){       
        const modalRef = this.modalService.open(UserListModal,{
            windowClass:'animated fadeInDown user-list-modal'
        });   
        modalRef.componentInstance.addUserToChat.subscribe(($event)=>{
            this.hubService.joinChat($event,this.chatId);
        });
        const modal = modalRef.componentInstance as UserListModal;
        this.apiService.getUsers().subscribe(data=>modal.users = data);
    }

    openUserProfileModal(){
        const modalRef = this.modalService.open(UserProfileModal,{
            windowClass:'animated fadeInDown'
        })
        let modal =modalRef.componentInstance as UserProfileModal;
        modalRef.componentInstance.uploadAvatar.subscribe(($event)=>{
            this.apiService.uploadAvatar($event);
        })
        this.apiService.getUserProfile(this.authService.getUserId()).subscribe(data=>modal.userProfile = data);
    }

    openNewChatModal(){
        const modalRef = this.modalService.open(newChatModal,{
            windowClass:'animated fadeInDown',
            size:'sm',
            centered:true
        }).result.then(chatName=>{
            this.apiService.createChat(chatName).subscribe(chat=>this.chatDialogs.push(chat));
        });
    }

    openChatDetailsModal(){
        const modalRef = this.modalService.open(ChatDetailModal,{
           windowClass:'animated fadeInDown' 
        });
        modalRef.componentInstance.addMember.subscribe(()=>{
            this.openUsersModal();
        });
        let modal = modalRef.componentInstance as ChatDetailModal;
        modal.chatName = this.chatName;
        this.apiService.getChatUsers(this.chatId).subscribe(users=>modal.members = users);
    }



    sendMessage(message:string){
        console.log(this.authService.getUserName());
        this.hubService.sendMessage({message:message,userName:this.authService.getUserName(),chatId:this.chatId});
    }


    ngOnDestroy(): void {
        this.hubService.disconect();
    }
}