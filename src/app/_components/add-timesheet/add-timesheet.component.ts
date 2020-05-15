import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService } from "@/_services";
import { User, Timesheet, TimesheetActivity, Project,Location } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { DatePipe, Time } from '@angular/common';
import { Converter } from "@/_helpers/converter";
import { AnyARecord } from "dns";

@Component({ templateUrl: 'add-timesheet.component.html' })

export class AddTimesheetComponent implements OnInit {

    addTimesheetActivityForm: FormGroup;
    //loading = false;
    submitted = false;
    currentUser: User;
    currentUserSubscription: Subscription;
    timesheet:Timesheet=new Timesheet();
    timesheetActivity:TimesheetActivity=new TimesheetActivity();
    project:Project=new Project();
    projects:Project[]=[];
    locations:Location[]=[];
    convertedDate:string;
    convertedTime:string;
    variable:Timesheet=new Timesheet();
   // ts = new timespan.TimeSpan();
    

    constructor(
        private formBuilder: FormBuilder,
        private authenticationService:AuthenticationService,
        private projectService:ProjectService,
        private locationService:LocationService,
        private timesheetService:TimesheetService,
        private timesheetActivityService:TimesheetActivityService,
        private converter:Converter
        //public datepipe: DatePipe

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });
    }

    ngOnInit()
    {
        this.addTimesheetActivityForm = this.formBuilder.group({
            Date: ['',Validators.required],
            StartTime: ['', Validators.required],
            EndTime: ['', Validators.required],
            Break: ['', Validators.required],
            Project:['', Validators.required],
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

    

    onSubmit()
    {
        this.submitted=true;
        
        
        // stop here if form is invalid
        if (this.addTimesheetActivityForm.invalid) {
            console.log("invalid");
            return;
        }  

        this.timesheet.Date=this.f.Date.value;
        this.timesheet.StartTime=this.converter.convertDate(this.f.Date.value,this.f.StartTime.value);
        this.timesheet.EndTime=this.converter.convertDate(this.f.Date.value,this.f.EndTime.value);
        this.timesheet.BreakTime=this.f.Break.value;
        this.timesheet.IdUser=this.currentUser.idUser;
        this.timesheet.IdLocation= this.f.Location.value;

        //console.log(this.timesheet);
        this.timesheetService.add(this.timesheet).pipe(first())
        .subscribe(
            data => {
                //this.alertService.success('Added timesheet successfully', true);
                //data=timesheet.IdTimesheet;
                this.timesheetActivity.IdTimesheet=data as number;
               
                this.timesheetActivity.IdProject=this.f.Project.value;
                this.timesheetActivity.Comments=this.f.Comments.value;
                this.timesheetActivity.WorkedHours=this.converter.convertTimeSpan(this.f.StartTime.value,this.f.EndTime.value,this.f.Break.value);
                //console.log( this.timesheetActivity);
               
        
                console.log("success");
            },
            error => {
                //this.alertService.error(error);
            });

            console.log(this.timesheetActivity);
            this.timesheetActivityService.add(this.timesheetActivity).pipe(first())
            .subscribe(
                dat=> {
                    //this.alertService.success('Added timesheetActivity successfully', true);
                    console.log(dat);
                    console.log("activity added");
                },
                error => {
                    //this.alertService.error(error);
                });
       
        
       
    }

}