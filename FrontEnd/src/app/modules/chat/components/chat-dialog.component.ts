import { ChatDialog } from './../../../shared/models/chatDialog.model';
import { Component, Input, HostListener, Output, EventEmitter } from "@angular/core";


@Component({
    selector:"chat-dialog",
    templateUrl:"./chat-dialog.component.html",
    styleUrls:["./chat-dialog.component.css"]
})
export class ChatDialogComponent{

    @Input("chat-info")
    chatInfo:ChatDialog;

    @Output('selectChat')
    selectChat = new EventEmitter<any>();

    @HostListener('click')
    openChat(){
        this.selectChat.emit({
            chatId:this.chatInfo.id,
            chatName:this.chatInfo.name
        });
    }
}