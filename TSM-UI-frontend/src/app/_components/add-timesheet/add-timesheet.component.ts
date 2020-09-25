import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators, FormArray} from '@angular/forms';
import { AuthenticationService,LocationService,TimesheetActivityService,AlertService, TimesheetService ,ProjectAssignmentsService} from "@/_services";
import { User, Timesheet, TimesheetActivity, Project,Location} from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { Converter } from "@/_helpers/converter";

@Component({ templateUrl: 'add-timesheet.component.html' ,styleUrls: ['add-timesheet-style.css']})

export class AddTimesheetComponent implements OnInit {

    //for timesheet form
    addTimesheetForm: FormGroup;//form builder
    submittedSave=false;//check for errors
    timesheet:Timesheet=new Timesheet();//for adding user activity

    //check variables
    checkHours2=false;//check if worked time with activities coincide
    checkTime=false;//check if end time si before start time

    //for activity form
    addTimesheetActivityForm:FormGroup;//form builder
    submitted = false;//for checking errors
    
    activities:TimesheetActivity[]=[];//activity array
    timesheetId:number;//for adding timesheet id
    checkWorkedHours=false;
    
    //current user
    currentUser: User;
    currentUserSubscription: Subscription;
    
    projects:Project[]=[];//for getting all projects
    locations:Location[]=[];//for getting all locations 

    //moments.js
    moment = require('moment');
      
    constructor(
        private formBuilder: FormBuilder,
        private authenticationService:AuthenticationService,
        private projectAssignmentsService:ProjectAssignmentsService,
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
            TotalWorkedHours:['0 hours 0 minutes'],
            timesheetActivities:this.formBuilder.array([],[Validators.required])   
         });

        //getting all projects and locations
        this.getAllProjects();
        this.getAllLocations();
    }

     //save records btn
     btnSaveRecords()
     {
         this.submittedSave=true;
 
         // stop here if form is invalid
         if (this.addTimesheetForm.invalid) 
         {     
             return;   
         }
 
         //validates time input
         this.validateTime();
    
         //adding attributes to timesheet object
         this.addAttributes();
        
         //adding timesheet to database
         this.addTimesheet();
 
         //resets variables
         this.checkHours2=false;
         this.checkTime=false;
         this.submittedSave=false;
     }

    //validates time input
    validateTime()
    {
        //init start time end time and break as moments and duration
        var start=this.moment(this.f.StartTime.value,"HH:mm");
        var end=this.moment(this.f.EndTime.value,"HH:mm");
        var br=this.moment.duration(this.f.Break.value);
       
        //validates if end time is before start time
        if(end.isBefore(start))
        {
            console.log("invalid5");
            this.checkTime=true;           
            return;
            
        }        
              
        //calculates worked hours from the timesheet form
        var totalWorked=this.moment.duration(end.diff(start)).subtract(br);
        
        //chacks if total worked hours from activities are the same with timesheet
        if(totalWorked!=this.calculateTotalWorkedHours())
        {
            console.log("invalid7");
            this.checkHours2=true;
            this.alertService.error("The time you registerd on the timesheet does not coincide with added activities");
            return;
        }
    }

    addAttributes()
    {
        //adding attributes to timesheetActivityView 
        this.timesheet.Date=this.f.Date.value;
        this.timesheet.StartTime=this.converter.convertDate(this.f.Date.value,this.f.StartTime.value);
        this.timesheet.EndTime=this.converter.convertDate(this.f.Date.value,this.f.EndTime.value);
        this.timesheet.BreakTime=this.converter.convertTime(this.f.Break.value);
        this.timesheet.IdUser=this.currentUser.idUser;
        this.timesheet.IdLocation= this.f.Location.value;

       for(let i=0;i< this.timesheetActivities.length;i++)
       {

           var timesheetActivity=this.timesheetActivities.at(i) as FormGroup;

           let activity:TimesheetActivity=new TimesheetActivity();//activity object

           //sets the value of activity form group
           activity.WorkedHours=this.converter.convertTime(timesheetActivity.controls.WorkedHours.value);
           activity.IdProject=timesheetActivity.controls.Project.value;
           activity.Comments=timesheetActivity.controls.Comments.value;
           
           //pushes to from array
           this.activities.push(activity);
        
       }

    }

    addActivity()
    {
        console.log(this.activities);
         //adding activities
         for(let activity of this.activities)
         {
            activity.IdTimesheet=this.timesheetId;
            console.log(this.timesheetId);
            console.log(activity);
            this.timesheetActivityService.add(activity).pipe(first())
                  .subscribe(
                     data => {
                      this.alertService.success('Added timesheet activity successfully', true);
                      console.log("activity added");
                      },
                     error => {
                      this.alertService.error(error);
                      console.log("error activity added");
                     });
         }

         this.activities=[];
    }

    addTimesheet()
    {
        //add function from timesheet Service
        this.timesheetService.add(this.timesheet).pipe(first())
        .subscribe(
            data => {
                this.alertService.success('Added timesheet successfully', true);
                this.timesheetId=data as number;            
                this.addActivity();
                console.log("timesheet added");},
            error => {
                this.alertService.error(error);
                console.log("timesheet added error")});
    }

     //generate new activity form btn
     btnAddActivity()
     {      
         //init form with validators      
         this.timesheetActivities.push(this.formBuilder.group({
             WorkedHours:this.formBuilder.control('',[Validators.required]),
             Project:this.formBuilder.control('',[Validators.required]),
             Comments:this.formBuilder.control(''),
         }));
         
     }
 
     btnRemove(i:number)
     {
         //init control from the form array
         const control = <FormArray>this.addTimesheetForm.controls['timesheetActivities'];
        
         var totalWorkedHours=this.calculateTotalWorkedHours();
 
         var x=this.timesheetActivities.at(i) as FormGroup;
         totalWorkedHours-=this.moment.duration(this.converter.convertTime(x.controls.WorkedHours.value));//add to total of worked hours
 
         //init hours and minutes
         var hour=this.moment.duration(totalWorkedHours).hours();
         var minutes=this.moment.duration(totalWorkedHours).minutes();
 
         //set value to from control
         this.f.TotalWorkedHours.setValue(hour+" hours "+ minutes+" minutes");
 
         control.removeAt(i);
       
     }
 
     
 

    getAllLocations()
    {
        this.locationService.getAll().pipe(first()).subscribe(locations=>{this.locations=locations; console.log("GET request succesfully done");});
    }

    getAllProjects()
    {
        this.projectAssignmentsService.getAll(this.currentUser.idUser).pipe(first()).subscribe(projects=>{this.projects=projects; console.log("GET request succesfully done");});
    }

    // convenience getter for easy access to form fields
    get f() { return this.addTimesheetForm.controls; }

    //easy access to form array
    get timesheetActivities(): FormArray 
    {
        return this.addTimesheetForm.get('timesheetActivities') as FormArray;
    }

    //validates time on change event
    checkDifference()
    {
        //init start time as moment
        var start=this.moment(this.addTimesheetForm.controls.StartTime.value,"HH:mm");

         //init end time as moment
        var end=this.moment(this.addTimesheetForm.controls.EndTime.value,"HH:mm");
       
        //check if end time is before start time
        if(end.isBefore(start))
        {
            
            this.checkTime=true;
               
        }
        else
        {
            this.checkTime=false;
                      
        }
    }

    //sets total worked hours on change events
    setTotalWorkedHours()
    {
        var totalWorkedHours=this.moment.duration(0);//init duration for total worked hours

        //parse form array
        for(let i=0;i< this.timesheetActivities.length;i++)
        {
            //get activity as form group
            var timesheetActivity=this.timesheetActivities.at(i) as FormGroup;

            //calculates total worked hours when activities are added
            //init worked hours as duration
            var workedHoursOnActivity=this.moment.duration(this.converter.convertTime(timesheetActivity.controls.WorkedHours.value));
            totalWorkedHours+=workedHoursOnActivity;//add to total of worked hours

            //init hours and minutes
            var hour=this.moment.duration(totalWorkedHours).hours();
            var minutes=this.moment.duration(totalWorkedHours).minutes();

            //set value to from control
            this.f.TotalWorkedHours.setValue(hour+" hours "+ minutes+" minutes");
         
        }
    }

    //calculates total worked hours at the moment of the execution
    calculateTotalWorkedHours()
    {
       //calculates total hours
       var workedHours=this.moment.duration(0);//init total worked hours
       //parses the form array and calculates total worked hours

       for(let i=0;i< this.timesheetActivities.length;i++)
       {
          var castedTimesheetActivity=this.timesheetActivities.at(i) as FormGroup;

          var worked=this.moment.duration(this.converter.convertTime(castedTimesheetActivity.controls.WorkedHours.value));//init worked hours as duration
          
          workedHours+=worked;
        
       }

       return workedHours;
    }   

}