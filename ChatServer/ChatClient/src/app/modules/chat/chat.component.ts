import { ChatUser } from 'src/app/shared/models/ChatUser.model';
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
    userPage:number;
    chatHistoryPage:number;

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
            this.chatMessages.push(msg);
            let chat = this.chatDialogs.find((chat)=>chat.name === this.chatName);
            chat.lastMessageDate = msg.messageDate;
            chat.lastMessageText = msg.messageText;
            chat.lastMessageUserName = msg.userName;
        });
        this.JoinChatNotification = this.hubService.chat.subscribe(chat=>{
            this.chatDialogs.push(chat);
        })
        this.hubService.connect();
        this.chatHistoryPage = 1;
        
    }

    openChat(chatInfo:any){
        this.isChatOpened = true;
        this.chatMessages = [];
        this.chatId = chatInfo.chatId;
        this.chatName = chatInfo.chatName;
        this.apiService.getChatMessages(this.chatId,this.chatHistoryPage).subscribe(data=>this.chatMessages = data);
        this.hubService.joinChat(this.authService.getUserId(),this.chatId);
    }

    closeChat(){
        this.isChatOpened = false;
    }

    loadMoreMessages(){
        this.chatHistoryPage++;
        this.apiService.getChatMessages(this.chatId,this.chatHistoryPage).subscribe(messages=>{
            messages.map(message=>this.chatMessages.unshift(message));
        });
    }

    openUsersModal(){
        this.userPage = 1;      
        const modalRef = this.modalService.open(UserListModal,{
            windowClass:'animated fadeInDown user-list-modal'
        });   
        const modal = modalRef.componentInstance as UserListModal;
        this.apiService.getUsers(this.userPage).subscribe(data=>{
            modal.users = data;
            console.log(data);
        });
        modalRef.componentInstance.addUserToChat.subscribe(($event)=>{
            this.hubService.joinChat($event,this.chatId);
        });
        modalRef.componentInstance.loadMoreUsers.subscribe(()=>{
            this.userPage++;
            this.apiService.getUsers(this.userPage).subscribe(data=>{
                data.map(user=>modal.users.push(user));
            })
        })
       
    }

    openUserProfileModal(userId?:string){
        const modalRef = this.modalService.open(UserProfileModal,{
            windowClass:'animated fadeInDown'
        })
        let modal =modalRef.componentInstance as UserProfileModal;
        modal.isCurrentUser = userId?false:true;
        modalRef.componentInstance.uploadAvatar.subscribe(($event)=>{
            this.apiService.uploadAvatar($event);
        })
        this.apiService.getUserProfile(userId || this.authService.getUserId()).subscribe(data=>modal.userProfile = data);
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