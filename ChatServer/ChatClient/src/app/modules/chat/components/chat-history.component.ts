import { EventEmitter } from '@angular/core';
import { AuthService } from './../../../core/authentication/auth.service';
import { Input, Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { ChatMessage } from 'src/app/shared/models/chat-message.model';

@Component({
  selector: 'chat-history',
  templateUrl: './chat-history.component.html',
  styleUrls: ['./chat-history.component.css']
})
export class ChatHistoryComponent{
 
  @Input()
  messages:ChatMessage[];

  @Input()
  userName:string;

  @Output('openUserProfile')
  openUserProfile = new EventEmitter<string>();

  @Output()
  loadMoreMessages = new EventEmitter();


  openUserProfileHandler(userId:string){
    this.openUserProfile.emit(userId);
  }

  loadMoreMessagesHandler(){
    this.loadMoreMessages.emit();
  }

  getKey(index:number,message:ChatMessage){
    return message.id;
  }
}
