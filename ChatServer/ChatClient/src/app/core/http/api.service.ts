import { EditUserModel } from './../../shared/models/editUser.model';
import { catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable, throwError, of } from "rxjs";
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

    uploadAvatar(avatar:File,onAvatarUpdated:any){
        let avatarUrl:string;
        let fileReader:FileReader = new FileReader();
        fileReader.onload = (event)=>{
            let base64img = fileReader.result.slice(23) as string;
            let extention = avatar.type.slice(6);
            if(extention ==="jpeg"){
                extention = "jpg";
            }
            return this.http.post(this.baseUrl+`/user/avatar`,{
                base64Avatar:base64img,
                extention:extention
            }).subscribe((url:string)=>{
                onAvatarUpdated(url);
            })
        }
        fileReader.readAsDataURL(avatar);
    }

    deleteAvatar():Observable<string>{
        return this.http.delete(this.baseUrl +"/user/avatar") as Observable<string>;
    }

    updateUser(userModel:EditUserModel){
        return this.http.put(this.baseUrl+"/user/profile",userModel).toPromise().catch(error=>{
            console.log(error);
        });
    }
}