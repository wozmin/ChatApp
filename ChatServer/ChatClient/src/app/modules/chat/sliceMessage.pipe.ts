import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name:"sliceMessage"
})
export class SliceMessagePipe implements PipeTransform{
   
    transform(value: string) {
        return value.length>10?value.slice(0,10)+"...":value;
    }

}