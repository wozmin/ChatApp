import { RouterModule, Routes } from '@angular/router';
import { EditUserProfileComponent } from './editUserProfile.component';
import { CoreModule } from '../../core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from "@angular/core";
import { UserProfileComponent } from './userProfile.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faEnvelope, faChild, faAddressBook, faTimes, faEdit } from '@fortawesome/free-solid-svg-icons';
import { ModalModule } from '../chat/modals/modal.module';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';

library.add(faEnvelope,faChild,faAddressBook,faTimes,faEdit)

const routes:Routes=[
    {
        path:'',component:UserProfileComponent,
    },
    {
        path:'edit',component:EditUserProfileComponent
    }
]

@NgModule({
    imports:[CommonModule,FormsModule,FontAwesomeModule,CoreModule,NgbModalModule,ModalModule,ReactiveFormsModule,
        RouterModule.forChild(routes)],
    declarations:[UserProfileComponent,EditUserProfileComponent],
    exports:[UserProfileComponent,EditUserProfileComponent]
})
export class UserProfileModule{

}