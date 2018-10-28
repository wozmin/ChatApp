import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { ChatDialog } from 'src/app/shared/models/chatDialog.model';
import { ChatMessage } from 'src/app/shared/models/chat-message.model';
import { ChatUser } from 'src/app/shared/models/ChatUser.model';
import { UserProfile } from 'src/app/shared/models/UserProfile.model';

@Injectable()
export class APIService{
    baseUrl:string;
    auth_token:string;
    userName:string;
    userId:string;


    constructor(private http:HttpClient){
        this.baseUrl = "http://localhost:53809/api";
    }

   
    getChats(userId:string):Observable<ChatDialog[]>{
        return this.http.get(this.baseUrl+`/chat/${userId}`) as Observable<ChatDialog[]>;
    }

    getChatMessages(chatId:number,page:number):Observable<ChatMessage[]>{
        return this.http.get(this.baseUrl+`/chat/${chatId}/message?page=${page}`) as Observable<ChatMessage[]>;
    }

    getUsers(page:number):Observable<ChatUser[]>{
        return this.http.get(this.baseUrl+`/user?page=${page}`) as Observable<ChatUser[]>;
    }

    createChat(chatName:string):Observable<ChatDialog>{
        return this.http.post(this.baseUrl+`/chat?chatName=${chatName}`,null) as Observable<ChatDialog>;
    }

    getUserProfile(userId:string):Observable<UserProfile>{
        return this.http.get(this.baseUrl+`/user/${userId}/profile`) as Observable<UserProfile>;
    }

    getChatUsers(chatId:number):Observable<ChatUser[]>{
        return this.http.get(this.baseUrl+`/chat/${chatId}/member`) as Observable<ChatUser[]>;
    }

    uploadAvatar(avatar:File){
        let formData = new FormData();
        formData.set('avatar',avatar);
        console.log(formData);
        console.log(avatar);
        return this.http.post(this.baseUrl+'/user/avatar',formData).toPromise().then(res=>{
            console.log(res);
        }).catch(error=>{
            console.log(error);
        });
        
    }


}