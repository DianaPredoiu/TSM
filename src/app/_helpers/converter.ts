import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class Converter {
     

    convertDate(currentDate:string,time:string)
    {
        return currentDate.concat("T").concat(time);
    }

    
}