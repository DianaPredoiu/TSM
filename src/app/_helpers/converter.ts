import { Injectable } from "@angular/core";
import { Time } from "@angular/common";
import { TimeInterval } from "rxjs/internal/operators/timeInterval";

@Injectable({ providedIn: 'root' })
export class Converter {
     

    convertDate(currentDate:string,time:string)
    {
        return currentDate.concat("T").concat(time);
    }

    convertTime(breakTime:string)
    {
        return breakTime.concat(":00");
    }
}