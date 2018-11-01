import { AuthService } from 'src/app/core/authentication/auth.service';
import { HubService } from './core/services/hub.service';
import { Component, OnInit } from "@angular/core";

@Component({
    selector:"app-root",
    templateUrl:"./app.component.html",
    styleUrls:["./app.component.css"]
})
export class AppComponent implements OnInit{

    constructor(
        private hubService:HubService,
        private authService:AuthService
        ){

    }

    ngOnInit(): void {
        if(!this.authService.isTokenExpired()){
            this.hubService.connect();
        }   
    }

    ngOnDestroy(): void {
        this.hubService.disconect();
    }
}