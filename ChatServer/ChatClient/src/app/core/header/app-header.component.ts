import { AuthService } from 'src/app/core/authentication/auth.service';
import { Component, Input, Output, OnInit, EventEmitter } from "@angular/core";
import { Router } from '@angular/router';

@Component({
    selector:"app-header",
    templateUrl:"./app-header.component.html",
    styleUrls:["./app-header.component.css"]
})
export class AppHeaderComponent{

   constructor(private authService:AuthService,private router:Router){}
   
   isMenuVisible:boolean = false;
   userId:string;

   ngOnInit(): void {
       this.userId = this.authService.getUserId();
   }

   @Input('chat-name')
   chatName:string;

   @Input()
   showBackBtn:boolean = false;

    @Output('createChat')
    createChat = new EventEmitter();

    @Output()
    closeChat = new  EventEmitter();

    @Output('openChatDetails')
    openChatDetails = new EventEmitter();

    
    openChatDetailsHandler(){
        this.openChatDetails.emit();
    }

    closeChatHandler(){
        this.closeChat.emit();
    }


    toggleMenu(){
        this.isMenuVisible = !this.isMenuVisible;
    }

    createChatHandler(){
        this.createChat.emit();
    }

    logout(){
        this.authService.logout();
        this.router.navigateByUrl("/login");
    }

}