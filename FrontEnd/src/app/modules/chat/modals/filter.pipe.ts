import { ChatUser } from './../../../shared/models/ChatUser.model';
import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name:"filter"
})
export class FilterPipe implements PipeTransform{
    
    transform(items: ChatUser[], searchText:string) {
        if (!items) return [];
        if(!searchText) return items;

        searchText = searchText.toLowerCase();
        return items.filter(item=>item.userName.toLowerCase().includes(searchText));
    }
    
}