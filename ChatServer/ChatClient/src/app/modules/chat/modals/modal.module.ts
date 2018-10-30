import { FilterPipe } from './filter.pipe';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { FormsModule } from '@angular/forms';
import { library } from '@fortawesome/fontawesome-svg-core';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from "@angular/core";
import { ChatDetailModal } from "./chatDetailModal.component";
import { UserListModal } from "./userListModal.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSearch, faCamera, faUser, faUsers} from '@fortawesome/free-solid-svg-icons';
import {  NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { newChatModal } from './newChatModal.component';
import { EditUserModal } from './editUserModal.component';


library.add(faSearch,faCamera,faUser,faUsers);

@NgModule({
    imports:[BrowserModule,FontAwesomeModule,NgbModalModule,FormsModule,InfiniteScrollModule],
    declarations:[ChatDetailModal,UserListModal,newChatModal,FilterPipe,EditUserModal],
    entryComponents:[ChatDetailModal,UserListModal,newChatModal,EditUserModal],
    exports:[newChatModal]
})
export class ModalModule{
    
}