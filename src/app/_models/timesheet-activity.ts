import { Timesheet } from "./timesheet";
import { Project } from "./project";
import { Time } from "@angular/common";

export class TimesheetActivity
{
    IdTimesheetActivity:number;
    IdTimesheet:number;
    IdProject:number;
    Comments:string;
    WorkedHours:string;
}