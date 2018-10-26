import { LoginComponent } from './app/modules/login/login.component';
import { Routes, RouterModule, Router } from "@angular/router";
import { AuthGuard } from './app/core/guards/auth.guard';
import { ChatComponent } from './app/modules/chat/chat.component';
import { NgModule } from '@angular/core';

const routes:Routes = [
    {path:'',component:ChatComponent,canActivate:[AuthGuard]},
    {path:'login',component:LoginComponent},
    {path:'**',redirectTo:'login'}
]

@NgModule({
    imports:[RouterModule.forRoot(routes)],
    exports:[RouterModule]
})
export class AppRoutingModule{}