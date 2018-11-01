import { ChatUser } from 'src/app/shared/models/ChatUser.model';
import { ChatDetailModal } from './modals/chatDetailModal.component';
import { UserProfile } from './../../shared/models/UserProfile.model';
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
import { NotifierService } from 'angular-notifier';


@Component({
    selector:"chat",
    templateUrl:"./chat.component.html",
    styleUrls:["./chat.component.css"]
})
export class ChatComponent implements OnInit{

    chatId:number;
    chatName:string;
    chatDialogs:ChatDialog[];
    chatMessages:ChatMessage[] = [];
    chatUsers:ChatUser[] = [];
    isChatOpened:boolean = false;
    userName:string;
    MessageNotification:Subscription;
    AddToChatNotification:Subscription;
    JoinChatNotification :Subscription;

    userPage:number;
    chatHistoryPage:number;

    constructor(
        private apiService:APIService,
        private hubService:HubService,
        private authService:AuthService,
        private modalService:NgbModal,
        private notifierService:NotifierService){
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
        this.AddToChatNotification = this.hubService.chat.subscribe(chat=>{
            this.chatDialogs.push(chat);
            this.notifierService.notify('success','User was added to chat successfully');
        })
        this.JoinChatNotification = this.hubService.user.subscribe(user=>{
            this.chatUsers.push(user);
            this.notifierService.notify('success','User was added successfully');
        })
    }

    openChat(chatInfo:any){
        this.chatHistoryPage = 1;
        this.isChatOpened = true;
        this.chatMessages = [];
        this.chatId = chatInfo.chatId;
        this.chatName = chatInfo.chatName;
        this.apiService.getChatMessages(this.chatId,this.chatHistoryPage).subscribe(data=>{
            data.map(msg=>this.chatMessages.unshift(msg));
        });
        this.hubService.joinChat(this.authService.getUserId(),this.chatId);
    }

    closeChat(){
        this.isChatOpened = false;
    }

    loadMoreMessages(){
        this.chatHistoryPage++;
        this.apiService.getChatMessages(this.chatId,this.chatHistoryPage).subscribe(messages=>{
            messages.map(message=>this.chatMessages.unshift(message));
            console.log(this.chatMessages);
        });
    }

    openUsersModal(){
        this.userPage = 1;      
        const modalRef = this.modalService.open(UserListModal,{
            windowClass:'animated fadeInDown user-list-modal'
        });   
        const modal = modalRef.componentInstance as UserListModal;
        modal.isAddBtnVisible = true;
        modalRef.componentInstance.addUserToChat.subscribe(($event)=>{
            this.hubService.joinChat($event,this.chatId);
        });
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
        this.modalService.dismissAll();
    }
}