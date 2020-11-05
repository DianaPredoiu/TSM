import { TimesheetActivity } from "./timesheet-activity";

export class TimesheetUpdate{

    IdTimesheet:number;
    IdLocation:number;
    IdUser:number;
    Date:string;
    StartTime:string;
    EndTime:string; 
    BreakTime:string;
    timesheetActivities:TimesheetActivity[];
}