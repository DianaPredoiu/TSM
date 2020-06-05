import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetActivityService,AlertService, TimesheetService } from "@/_services";
import { User, Timesheet, TimesheetActivity, Project,Location } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { Converter } from "@/_helpers/converter";

@Component({ templateUrl: 'add-timesheet.component.html' ,styleUrls: ['add-timesheet-style.css']})

export class AddTimesheetComponent implements OnInit {

    //for timesheet form
    addTimesheetForm: FormGroup;//form builder
    submittedSave=false;//check for errors
    timesheet:Timesheet=new Timesheet();//for adding user activity

    //for activity form
    addTimesheetActivityForm:FormGroup;//form builder
    submitted = false;//for checking errors
    activity:TimesheetActivity=new TimesheetActivity();//activity object
    activities:TimesheetActivity[]=[];//activity array
    timesheetId:number;//for adding timesheet id
    
    currentUser: User;
    currentUserSubscription: Subscription;
    
    projects:Project[]=[];//for getting all projects
    locations:Location[]=[];//for getting all locations 

    //for generating activity forms
    timesheetActivities:number[]=[];
      
    constructor(
        private formBuilder: FormBuilder,
        private authenticationService:AuthenticationService,
        private projectService:ProjectService,
        private locationService:LocationService,
        private alertService:AlertService,
        private timesheetService:TimesheetService,
        private timesheetActivityService:TimesheetActivityService,
        private converter:Converter

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });
    }

    ngOnInit()
    {
        //init form with validators
        this.addTimesheetForm = this.formBuilder.group({
            Date: ['',Validators.required],
            StartTime: ['', Validators.required],
            EndTime: ['', Validators.required],
            Break: ['', Validators.required],           
            Location:['', Validators.required],
            
        });

        //getting all projects and locations
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
    get f() { return this.addTimesheetForm.controls; }

    // convenience getter for easy access to form fields
    get fa() { return this.addTimesheetActivityForm.controls; }

    //save records btn
    btnSaveRecords()
    {
        this.submittedSave=true;
        
        
        // stop here if form is invalid
        if (this.addTimesheetForm.invalid) {
           
            return;
        }  

        //adding attributes to timesheetActivityView 
        this.timesheet.Date=this.f.Date.value;
        this.timesheet.StartTime=this.converter.convertDate(this.f.Date.value,this.f.StartTime.value);
        this.timesheet.EndTime=this.converter.convertDate(this.f.Date.value,this.f.EndTime.value);
        this.timesheet.BreakTime=this.converter.convertTime(this.f.Break.value);
        this.timesheet.IdUser=this.currentUser.idUser;
        this.timesheet.IdLocation= this.f.Location.value;
        

        //add function from timesheet Service
        this.timesheetService.add(this.timesheet).pipe(first())
        .subscribe(
            data => {
                this.alertService.success('Added timesheet successfully', true);
                console.log("timesheet success");
                this.timesheetId=data as number;
            },
            error => {
                this.alertService.error(error);
            });


        //adding activities
        for(let activity of this.activities)
        {
            activity.IdTimesheet=this.timesheetId;

            this.timesheetActivityService.add(activity).pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Added timesheet activity successfully', true);
                    console.log("success");
                },
                error => {
                    this.alertService.error(error);
                });
        }

    }

    //generate new activity form btn
    btnAddActivity()
    {      
        if(this.submitted)
        {
            return;
        }

        this.timesheetActivities.push(1);

         //init form with validators
         this.addTimesheetActivityForm = this.formBuilder.group({
            WorkedHours: ['',Validators.required],
            Project: ['', Validators.required],
            Comments: ['', Validators.required],
       
        });
    }


    //submit activity form btn
    btnSubmit()
    {
        this.submitted=true;

        //stop here if form is invalid
        if (this.addTimesheetActivityForm.invalid) {
           
            return;
        } 
        
        this.activity.WorkedHours=this.fa.WorkedHours.value;
        this.activity.IdProject=this.fa.Project.value;
        this.activity.Comments=this.fa.Comments.value;


        this.activities.push(this.activity);

        this.submitted=false;

    }

}