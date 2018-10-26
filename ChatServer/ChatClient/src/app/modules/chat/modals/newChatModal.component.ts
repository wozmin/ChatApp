import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component,EventEmitter, Output } from "@angular/core";


@Component({
    selector:'new-chat-modal',
    templateUrl:"./newChatModal.component.html",
    styleUrls:["./newChatModal.component.css"]
})
export class newChatModal{
    newChatName:string;
    constructor(public activeModal:NgbActiveModal){}
}