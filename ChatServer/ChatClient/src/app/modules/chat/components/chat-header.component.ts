import { Component, Input, Output, OnInit, EventEmitter } from "@angular/core";

@Component({
    selector:"chat-header",
    templateUrl:"./chat-header.component.html",
    styleUrls:["./chat-header.component.css"]
})
export class ChatHeaderComponent{

   constructor(){}
   
   isMenuVisible:boolean = false;

   @Input('chat-name')
   chatName:string;

   @Output('openUsersModal')
   openUsersModal = new EventEmitter();
 
   @Output('openUserProfileModal')
    openUserProfileModal = new EventEmitter()

    @Output('createChat')
    createChat = new EventEmitter();

    @Output('openChatDetails')
    openChatDetails = new EventEmitter();

    
    openChatDetailsHandler(){
        this.openChatDetails.emit();
    }


    toggleMenu(){
        this.isMenuVisible = !this.isMenuVisible;
    }

    openUsersModalHandler(){
        this.openUsersModal.emit();
    }

    openUserProfileModalHandler(){
        this.openUserProfileModal.emit();
    }

    createChatHandler(){
        this.createChat.emit();
    }

}