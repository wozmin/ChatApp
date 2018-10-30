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
    
    constructor(public activeModal:NgbActiveModal){}

    selectedUserId:string;
    searchUserName:string;

    @Input('users')
    users:ChatUser[];

    @Output()
    addUserToChat = new EventEmitter<string>();
    
    @Output()
    loadMoreUsers = new EventEmitter();

    ngOnInit(): void {
        this.users = [];
    }

    onScroll(){
        this.loadMoreUsers.emit();
    }

    addUserToChatHandler(){
        this.addUserToChat.emit(this.selectedUserId);
    }

    selectUser(id:string){
        this.selectedUserId = id;
    }
}