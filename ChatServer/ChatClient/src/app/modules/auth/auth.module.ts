import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SignInComponent } from './sign-in.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SignUpComponent } from './sign-up.component';

@NgModule({
    imports:[FormsModule,BrowserModule,ReactiveFormsModule],
    declarations:[SignInComponent,SignUpComponent],
    exports:[SignInComponent,SignUpComponent]
})
export class AuthModule{}