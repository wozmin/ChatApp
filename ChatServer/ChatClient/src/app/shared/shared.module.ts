import { ChatMessage } from './models/chat-message.model';
import { NgModule } from "@angular/core";
import { ChatDialog } from "./models/chatDialog.model";
import { Login } from "./models/login.model";

@NgModule({
    providers:[ChatDialog,Login,ChatMessage]
})
export class SharedModule{}