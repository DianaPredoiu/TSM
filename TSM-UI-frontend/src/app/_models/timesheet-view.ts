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

     
     // constructor(
     //      public idTimesheet: number,
     //      public date: Date,
     //      public startTime: Date,
     //      public endTime: Date,
     //      public breakTime:Date,
     //      public location:string,
     //      public idLocation:number,
     //      public idUser:number,
     //      public username:string,
     //      public timesheetActivities:TimesheetActivityView[]
     //    ) { }

       
    
}