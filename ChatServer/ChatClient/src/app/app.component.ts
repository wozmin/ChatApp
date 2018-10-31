import { HubService } from './core/services/hub.service';
import { Component } from "@angular/core";

@Component({
    selector:"app-root",
    templateUrl:"./app.component.html",
    styleUrls:["./app.component.css"]
})
export class AppComponent{

    constructor(private hubService:HubService){

    }

    ngOnInit(): void {
        this.hubService.connect();
    }

    ngOnDestroy(): void {
        this.hubService.disconect();
    }
}