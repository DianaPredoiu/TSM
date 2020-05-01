import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService } from "@/_services";
import { User, Timesheet, TimesheetActivity, Project,Location } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { DatePipe, Time } from '@angular/common';
import { TimesheetView } from "@/_models/timesheet-view";

@Component({ templateUrl: 'timesheet-view.component.html' })

export class ViewTimesheetComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    timesheet:Timesheet=new Timesheet();
    timesheetActivity:TimesheetActivity=new TimesheetActivity();
    projects:Project[]=[];
    locations:Location[]=[];
    timesheets:TimesheetView[]=[];
    show=false;

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
       this.getAllTimesheetActivities(this.currentUser.idUser);
    }

    getAllTimesheetActivities(userId:number)
    {
         this.timesheetService.getAllById(userId).pipe(first()).subscribe(timesheets=>{this.timesheets=timesheets;console.log("GET request succesfully done");});
    }

    onClick()
    {
        this.show=true;
    }
}