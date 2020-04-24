import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService } from "@/_services";
import { User, Timesheet, TimesheetActivity, Project,Location } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { DatePipe, Time } from '@angular/common';

@Component({ templateUrl: 'add-timesheet.component.html' })

export class AddTimesheetComponent implements OnInit {

    addTimesheetActivityForm: FormGroup;
    loading = false;
    submitted = false;
    currentUser: User;
    currentUserSubscription: Subscription;
    timesheet:Timesheet=new Timesheet();
    timesheetActivity:TimesheetActivity=new TimesheetActivity();
    projects:Project[]=[];
    locations:Location[]=[];
    convertedDate:string;
    convertedTime:string;

    constructor(
        private formBuilder: FormBuilder,
        private authenticationService:AuthenticationService,
        private projectService:ProjectService,
        private locationService:LocationService,
        private timesheetService:TimesheetService,
        private timesheetActivityService:TimesheetActivityService,
        public datepipe: DatePipe

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });
    }

    ngOnInit()
    {
        this.addTimesheetActivityForm = this.formBuilder.group({
            Date: ['', Validators.required],
            StartTime: ['', Validators.required],
            EndTime: ['', Validators.required],
            Break: ['', Validators.required],
            Projects:['', Validators.required],
            Location:['', Validators.required],
            Comments:['', Validators.required]
        });

        this.getAllProjects();
        this.getAllLocations();
    }

    getAllLocations()
    {
        this.locationService.getAll().pipe(first()).subscribe(locations=>{this.locations=locations; console.log("GET request succesfully done");});
    }

    getAllProjects()
    {
        this.projectService.getAll().pipe(first()).subscribe(projects=>{this.projects=projects; console.log("GET request succesfully done");});
    }

    // convenience getter for easy access to form fields
    get f() { return this.addTimesheetActivityForm.controls; }

    convertDate(time:Time)
    {
        return this.f.Date.value.concat("T").concat(time);
    }

    onSubmit()
    {
        this.submitted=true;
        
        // // stop here if form is invalid
        // if (this.addTimesheetActivityForm.invalid) {
        //     return;
        // }

        this.timesheet.Date=this.f.Date.value;
        this.timesheet.StartTime=this.convertDate(this.f.StartTime.value);  
        this.timesheet.EndTime=this.convertDate(this.f.EndTime.value);
        this.timesheet.BreakTime=this.convertDate(this.f.Break.value);
        this.timesheet.IdUser=this.currentUser.idUser;
        this.timesheet.IdLocation=2;
       
        console.log(this.timesheet);
        console.log(this.locations);
        this.timesheetService.add(this.timesheet);

        // this.timesheetActivity.IdProject=this.f.Project.value;
        // this.timesheetActivity.Comments=this.f.Comments.value;
        // this.timesheetActivity.IdTimesheet=this.timesheet.IdTimesheet;

        // this.timesheetActivityService.add(this.timesheetActivity);

    }

}