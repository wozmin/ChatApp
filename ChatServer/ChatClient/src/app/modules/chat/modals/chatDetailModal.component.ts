import { ChatUser } from 'src/app/shared/models/ChatUser.model';
import { Component, Input, Output, EventEmitter } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
    templateUrl:"./chatDetailModal.component.html",
    styleUrls:["./chatDetailModal.component.css"]
    
})
export class ChatDetailModal{
    @Input()
    chatName:string;

    @Input()
    members:ChatUser[];

    @Output()
    addMember = new EventEmitter();

    addMemberHandler(){
        this.addMember.emit();
    }

    constructor(public activeModal:NgbActiveModal){}
}