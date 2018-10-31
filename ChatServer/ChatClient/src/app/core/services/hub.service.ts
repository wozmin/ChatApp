import { ChatUser } from 'src/app/shared/models/ChatUser.model';

import { ChatMessage } from './../../shared/models/chat-message.model';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { Injectable } from "@angular/core";
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";
import { Subject } from 'rxjs';
import { ChatDialog } from 'src/app/shared/models/chatDialog.model';

@Injectable()
export class HubService {
    private hubConnection:HubConnection;
    message = new Subject<ChatMessage>();
    chat = new Subject<ChatDialog>();
    user = new Subject<ChatUser>();

    constructor(private authService:AuthService){
    }

    connect(){
        if(!this.hubConnection){
            this.hubConnection = new HubConnectionBuilder()
            .withUrl("http://localhost:53809/chat",{ accessTokenFactory: () =>this.authService.getToken() })
            .build();
        }

        this.hubConnection.on("SendMessage",(message:ChatMessage)=>{
            this.message.next(message);
        });

        this.hubConnection.on("JoinChat",(chat:ChatDialog)=>{
            this.chat.next(chat);
        });

        this.hubConnection.on("OnJoinedChat",(user:ChatUser)=>{
            this.user.next(user);
        })

        this.hubConnection.start().catch(error=>console.log(error));
    }


    sendMessage(message){
        this.hubConnection.invoke("SendMessage",message).catch(error=>console.log(error));
    }

    joinChat(userId,chatId){
        this.hubConnection.invoke("JoinChat",userId,chatId).catch(error=>console.log(error));
    }

    disconect(){
        if(this.hubConnection){
            this.hubConnection.stop();
            this.hubConnection = null;
        }
    }

}