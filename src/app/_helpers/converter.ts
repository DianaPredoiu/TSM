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

    
    validateTime(startTime:string,endTime:string)
    {
        if(startTime[0]=='0' && endTime[0]=='0')
        {
            
                if(startTime[1]>endTime[1])
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
        }
        if(startTime[0]!='0' && endTime[0]=='0')
        {
            return true;
        }

        if(startTime[0]=='0' && endTime[0]!='0')
        {
            return false;
        }
        
        if(startTime[0]!='0' && endTime[0]!='0')
        {
            var start_time=parseInt(startTime[0],10)*10+parseInt(startTime[1],10);
            var end_time=parseInt(endTime[0],10)*10+parseInt(endTime[1],10);

            if(start_time>end_time)           
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    validateHours(time:string)
    {
        
        if(time[0]=='0'&& parseInt(time[1],10)>=6 && parseInt(time[1],10)<=9)
        {
            return true;
        }
        if(time[0]!='0')
        {
            var _time=parseInt(time[0],10)*10+parseInt(time[1],10);
            if(_time >= 10 && _time<=22)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
   
    validateBreak(time:string)
    {
        if(parseInt(time[1])>1)
        {
            return false;
        }
        else if(time[0]=='0'&& parseInt(time[3])>5 && parseInt(time[4])<9)
            {
               return false;
            } 
            else
            {
               return true;
            }
    }

    validateWorkedHours(startTime:string,endTime:string,breakTime:string,workedHours:string)
    {
        var start_time:Time=(startTime as unknown)as Time;
        var end_time:Time=(endTime as unknown)as Time;
        var break_time:Time=(breakTime as unknown) as Time;
        var worked_hours:Time=(workedHours as unknown)as Time;

        if(worked_hours.hours > end_time.hours-start_time.hours-break_time.hours)
        {
            return false;
        }
        else
        {
            return true;
        }
       
    }
}
