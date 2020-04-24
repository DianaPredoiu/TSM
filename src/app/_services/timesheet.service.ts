import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Timesheet } from '@/_models';
import { first } from 'rxjs/operators';
import {AlertService} from './alert.service';
import {Router} from '@angular/router';

@Injectable({ providedIn: 'root' })
export class TimesheetService {

    constructor(private http: HttpClient,private alertService:AlertService,private router:Router) { }

    add(timesheet: Timesheet) {
        return this.http.post(`${config.apiUrl}/timesheets/create`, timesheet).pipe(first())
        .subscribe(
            data => {
                this.alertService.success('Added timesheet successfully', true);
                console.log("success");
            },
            error => {
                this.alertService.error(error);
            });
    }
    
}