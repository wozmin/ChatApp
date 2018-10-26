import { Component, Output, EventEmitter } from "@angular/core";

@Component({
    selector:"chat-form",
    templateUrl:"./chat-form.component.html",
    styleUrls:["./chat-form.component.css"]
})
export class ChatFormComponent{
    message:string;

    @Output('sendMessage')
    sendMessage = new EventEmitter<string>();

   
    submitForm(){
        this.sendMessage.emit(this.message);
        this.message = '';
  }
    
}