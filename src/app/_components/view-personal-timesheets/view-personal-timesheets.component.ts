import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService } from "@/_services";
import { User, Project } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { TimesheetView } from "@/_models/timesheet-view";
import { ActivatedRoute } from "@angular/router";

@Component({ templateUrl: 'view-personal-timesheets.component.html',styleUrls: ['table-style.css']})

export class ViewPersonalTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    timesheets:TimesheetView[]=[];
    chooseDate:FormGroup;
    sub:Subscription;
    id:number;//id for the user you want to get activities for
    date:any;
    projects:Project[]=[];

    constructor(
       
        private authenticationService:AuthenticationService,
        private projectService:ProjectService,
        private timesheetService:TimesheetService,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });

   
    }

    ngOnInit()
    {
        //gets the id form the route
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; });

        //gets projects for the current user
        this.getProjectsById(this.currentUser.idUser);

        //builds form with validators
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


    //gets activities by user id and date
    getAllTimesheetActivities(userId:number,date:string)
    {
        
            this.timesheetService.getAllById(userId,date).pipe(first()).subscribe(timesheets=>{
                this.timesheets=timesheets;
                 console.log("GET request succesfully done");
               
                });
        
            
    }

    //gets activities by user id ,date and project
    getByIdProject(userId:number,date:string,idProj:number)
    {
        this.timesheetService.getAllByIdProject(userId,date,idProj).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
    }

    onSubmit()
    {
        //gets date from form
        this.date=this.f.Date.value;

        //if we want to see a specified user's activities by date
        if(this.id)
        {
            this.getAllTimesheetActivities(this.id,this.date);
        }
        else
        {
            //if there is a projects selected
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