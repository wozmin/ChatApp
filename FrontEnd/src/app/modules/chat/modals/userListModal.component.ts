import { APIService } from './../../../core/http/api.service';
import { Component, Input, Output } from "@angular/core";
import { ChatUser } from "src/app/shared/models/ChatUser.model";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { EventEmitter } from '@angular/core';

@Component({
    templateUrl:"./userListModal.component.html",
    styleUrls:["./userListModal.component.css"],
})
export class UserListModal{
    
    constructor(
        public activeModal:NgbActiveModal,
        private apiService:APIService
        ){}

    selectedUserId:string;
    searchUserName:string;
    userPage:number;

    users:ChatUser[];

    @Input()
    isAddBtnVisible:boolean = false;

    @Output()
    addUserToChat = new EventEmitter<string>();
    
    ngOnInit(): void {
        this.userPage = 1;
        this.users = [];
        this.apiService.getUsers(this.userPage).subscribe((users:ChatUser[])=>{
            this.users = users;
        });
    }
    

    onScroll(){
        this.userPage++;
        this.apiService.getUsers(this.userPage).subscribe(users=>{
            users.map(user=>this.users.push(user));
        });
    }

    addUserToChatHandler(){
        this.addUserToChat.emit(this.selectedUserId);
    }

    selectUser(id:string){
        this.selectedUserId = id;
    }
}