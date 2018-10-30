import { RouterModule } from '@angular/router';
import { EditUserProfileComponent } from './editUserProfile.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from './../../core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { UserProfileComponent } from './userProfile.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faEnvelope, faChild, faAddressBook, faTimes, faEdit } from '@fortawesome/free-solid-svg-icons';
import { ModalModule } from '../chat/modals/modal.module';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

library.add(faEnvelope,faChild,faAddressBook,faTimes,faEdit)

@NgModule({
    imports:[BrowserModule,FormsModule,FontAwesomeModule,CoreModule,NgbModalModule,ModalModule,ReactiveFormsModule,RouterModule],
    declarations:[UserProfileComponent,EditUserProfileComponent],
    exports:[UserProfileComponent,EditUserProfileComponent]
})
export class UserProfileModule{

}