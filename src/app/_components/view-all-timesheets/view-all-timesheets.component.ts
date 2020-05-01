import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService } from "@/_services";
import { User, Timesheet, TimesheetActivity, Project,Location } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { DatePipe, Time } from '@angular/common';
import { TimesheetView } from "@/_models/timesheet-view";

@Component({ templateUrl: 'view-all-timesheets.component.html'})

export class ViewAllTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    timesheets:TimesheetView[]=[];

    constructor(
       
        private authenticationService:AuthenticationService,
        private projectService:ProjectService,
        private locationService:LocationService,
        private timesheetService:TimesheetService,
        private timesheetActivityService:TimesheetActivityService,
        private alertService:AlertService
        //public datepipe: DatePipe

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });
    }

    ngOnInit()
    {
        console.log(this.currentUser.idUser);
       this.getAllTimesheetActivities(this.currentUser.idUser);
       console.log(this.timesheets);
    }

    getAllTimesheetActivities(userId:number)
    {
         this.timesheetService.getAllById(userId).pipe(first()).subscribe(timesheets=>{this.timesheets=timesheets;console.log("GET request succesfully done");});
    }
}