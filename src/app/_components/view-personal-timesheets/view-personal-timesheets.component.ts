import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService } from "@/_services";
import { User, Timesheet, TimesheetActivity, Project,Location } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { DatePipe, Time } from '@angular/common';
import { TimesheetView } from "@/_models/timesheet-view";
import { tmpdir } from "os";
import { ActivatedRoute } from "@angular/router";

@Component({ templateUrl: 'view-personal-timesheets.component.html'})

export class ViewPersonalTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    timesheets:TimesheetView[]=[];
    chooseDate:FormGroup;
    sub:Subscription;
    id:number;
    date:any;
    projects:Project[]=[];
    //tm : Timesheet;

    constructor(
       
        private authenticationService:AuthenticationService,
        private projectService:ProjectService,
        private locationService:LocationService,
        private timesheetService:TimesheetService,
        private timesheetActivityService:TimesheetActivityService,
        private alertService:AlertService,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute
        //public datepipe: DatePipe

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });

   
    }

    ngOnInit()
    {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; });

        this.getProjectsById(this.currentUser.idUser);

        this.chooseDate = this.formBuilder.group({
            Date: ['',Validators.required],
            Project: ['',Validators.required]        
        });
    }

    getProjectsById(id:number)
    {
        this.projectService.getByUserId(id).pipe(first()).subscribe(projects=>{this.projects=projects;console.log("get req done");});

    }

    // convenience getter for easy access to form fields
    get f() { return this.chooseDate.controls; }

    getAllTimesheetActivities(userId:number,date:string)
    {
        
            this.timesheetService.getAllById(userId,date).pipe(first()).subscribe(timesheets=>{
                this.timesheets=timesheets;
                 console.log("GET request succesfully done");console.log(this.timesheets);
                //  this.tm.IdLocation = timesheets[0].IdLocation;
                });
        
            
    }

    getByIdProject(userId:number,date:string,idProj:number)
    {
        this.timesheetService.getAllByIdProject(userId,date,idProj).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
             console.log("GET request succesfully done");console.log(this.timesheets);
            //  this.tm.IdLocation = timesheets[0].IdLocation;
            });
    }

    onSubmit()
    {
        this.date=this.f.Date.value;
        if(this.id)
        {
            this.getAllTimesheetActivities(this.id,this.date);
        }
        else
        {
            if(this.f.Project.value)
            {
                this.getByIdProject(this.currentUser.idUser,this.date,this.f.Project.value);
            }
            else
            {
                this.getAllTimesheetActivities(this.currentUser.idUser,this.date);
            }
           
        }
      

    }
}