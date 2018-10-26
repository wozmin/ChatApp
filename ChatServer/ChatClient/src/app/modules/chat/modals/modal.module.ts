import { FormsModule } from '@angular/forms';
import { library } from '@fortawesome/fontawesome-svg-core';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from "@angular/core";
import { ChatDetailModal } from "./chatDetailModal.component";
import { UserListModal } from "./userListModal.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSearch, faCamera, faUser, faUsers } from '@fortawesome/free-solid-svg-icons';
import {  NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { UserProfileModal } from './userProfileModal.component';
import { newChatModal } from './newChatModal.component';


library.add(faSearch,faCamera,faUser,faUsers);

@NgModule({
    imports:[BrowserModule,FontAwesomeModule,NgbModalModule,FormsModule],
    declarations:[ChatDetailModal,UserListModal,UserProfileModal,newChatModal],
    entryComponents:[ChatDetailModal,UserListModal,UserProfileModal,newChatModal],
    exports:[newChatModal]
})
export class ModalModule{
    
}