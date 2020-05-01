import { HttpClient } from '@angular/common/http';
import { Timesheet, TimesheetActivity } from '@/_models';
import { first } from 'rxjs/operators';
import {AlertService} from './alert.service';
import {Router} from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class TimesheetActivityService {

    constructor(private http: HttpClient,private alertService:AlertService,private router:Router) { }

    add(timesheetActivity: TimesheetActivity) {
        return this.http.post(`${config.apiUrl}/TimesheetActivity/create`, timesheetActivity).pipe(first())
        .subscribe(
            data => {
                this.alertService.success('Added timesheetActivity successfully', true);
                console.log("activity added");
            },
            error => {
                this.alertService.error(error);
            });
    }
    
}