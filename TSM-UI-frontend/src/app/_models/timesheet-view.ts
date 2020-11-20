import { TimesheetActivityView } from "./timesheet-activity-view";

export class TimesheetView
{
     IdTimesheet:number;
     Date:Date;
     StartTime :Date;
     EndTime:Date;
     BreakTime:Date;
     Location:string;
     IdLocation:number;
     IdUser:number;
     Username:string;
     TimesheetActivities:TimesheetActivityView[]=[];
    
}