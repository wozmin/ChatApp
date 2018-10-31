import { RouterModule } from '@angular/router';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { ScrollChatDirective } from './scrollChat.directive';
import { SliceMessagePipe } from './sliceMessage.pipe';
import { CoreModule } from './../../core/core.module';
import { ChatFormComponent } from './components/chat-form.component';
import { ChatDialogComponent } from './components/chat-dialog.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ChatComponent } from './chat.component';
import { ChatHistoryComponent } from './components/chat-history.component';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faUser, faUsers, faAddressBook, faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { ModalModule } from './modals/modal.module';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { HubService } from 'src/app/core/services/hub.service';

library.add(faUser,faUsers,faAddressBook,faArrowLeft);

@NgModule({
    imports:[BrowserModule,FormsModule,FontAwesomeModule,ModalModule,NgbModalModule,InfiniteScrollModule,CoreModule,RouterModule],
    providers:[NgbActiveModal,HubService],
    declarations:[ChatComponent,ChatDialogComponent, ChatHistoryComponent,
        ChatHistoryComponent,ChatFormComponent,SliceMessagePipe,ScrollChatDirective],
    exports:[ChatComponent],
})
export class ChatModule{}