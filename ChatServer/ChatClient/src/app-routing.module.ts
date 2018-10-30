import { EditUserProfileComponent } from './app/modules/userProfile/editUserProfile.component';
import { UserProfileComponent } from './app/modules/userProfile/userProfile.component';
import { SignInComponent } from './app/modules/auth/sign-in.component';
import { Routes, RouterModule, Router } from "@angular/router";
import { AuthGuard } from './app/core/guards/auth.guard';
import { ChatComponent } from './app/modules/chat/chat.component';
import { NgModule } from '@angular/core';
import { SignUpComponent } from './app/modules/auth/sign-up.component';

const routes:Routes = [
    {path:'',component:ChatComponent,canActivate:[AuthGuard]},
    {path:'login',component:SignInComponent},
    {path:'sign-up',component:SignUpComponent},
    {path:'user/:id/profile', component:UserProfileComponent},
    {path:'user/:id/profile/edit', component:EditUserProfileComponent},
    {path:'**',redirectTo:'login'}
]

@NgModule({
    imports:[RouterModule.forRoot(routes,{useHash:true})],
    exports:[RouterModule]
})
export class AppRoutingModule{}