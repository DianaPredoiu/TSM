import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Timesheet, TimesheetView } from '@/_models';
import {AlertService} from './alert.service';
import {Router} from '@angular/router';
import { TimesheetObj } from '@/_models/timesheet-obj';

@Injectable({ providedIn: 'root' })
export class TimesheetService {

    constructor(private http: HttpClient,private alertService:AlertService,private router:Router) { }

    add(timesheet: Timesheet) {
        return this.http.post(`${config.apiUrl}/timesheets/create`, timesheet);        
    }

    getByGeneratedFilter(timesheetObj:TimesheetObj)
    {
        return this.http.post<TimesheetView[]>(`${config.apiUrl}/timesheets/filter`,timesheetObj);
    }

    data:TimesheetView=new TimesheetView();

}