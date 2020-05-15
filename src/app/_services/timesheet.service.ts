import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Timesheet, TimesheetView } from '@/_models';
import { first } from 'rxjs/operators';
import {AlertService} from './alert.service';
import {Router} from '@angular/router';

@Injectable({ providedIn: 'root' })
export class TimesheetService {

    constructor(private http: HttpClient,private alertService:AlertService,private router:Router) { }

    add(timesheet: Timesheet) {
        return this.http.post(`${config.apiUrl}/timesheets/create`, timesheet);        
    }

    getAllById(id:number,date:string)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/${id}/${date}`);
    }

    getAllByIdProject(id:number,date:string,idProj:number)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/${id}/${date}/${idProj}`);
    }
    
}