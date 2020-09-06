import { Directive, AfterViewChecked, ViewChild, ElementRef, OnInit } from "@angular/core";

@Directive({
    selector:'scrollChat'
})
export class ScrollChatDirective implements OnInit,AfterViewChecked{

    @ViewChild('scrollChat')
    private scollContainer:ElementRef;

    ngOnInit(): void {
        this.scrollToBottom();
    }

    ngAfterViewChecked(){
        this.scrollToBottom();
    }

    scrollToBottom(){
        this.scollContainer.nativeElement.scrollTop = this.scollContainer.nativeElement.scrollHeight;
    }
}