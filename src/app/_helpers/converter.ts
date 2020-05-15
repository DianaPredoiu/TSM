import { Injectable } from "@angular/core";
import { Time } from "@angular/common";
import { TimeInterval } from "rxjs/internal/operators/timeInterval";

@Injectable({ providedIn: 'root' })
export class Converter {
     

    convertDate(currentDate:string,time:string)
    {
        return currentDate.concat("T").concat(time);
    }

    convertTimeSpan(startTime:string,endTime:string,breakTime:string)
    {
        var counter:number=0;
        
        var start=startTime.split(":");
        var end=endTime.split(":");
        var br=breakTime.split(":");
        
        var startMinutes=parseInt(start[1]);
        var endMinutes=parseInt(end[1]);
        var brMinutes=parseInt(br[1]);
        var startHours=parseInt(start[0]);
        var endHours=parseInt(end[0]);
        var brHours=parseInt(br[0]);

       

        var timespan=this.convertToPositive(endMinutes-startMinutes);

        if(timespan<0)
        counter--;
        
       
        var min = timespan-brMinutes; 
        
        if(min<0)
        counter--;

        var hour = endHours-startHours-brHours;

        return this.formatTime( this.convertToPositive(hour+counter)) + ':' +this.formatTime(this.convertToPositive(min) ) ;

    }

    convertToPositive(num:number)
    {
        if(num<0)
        {
            return num *(-1);
        }
        else
        {
            return num;
        }
    }

    formatTime(num:number)
    {
        if(num <10)
         return "0"+num.toString();
         else
         return num.toString();
    }

    
}