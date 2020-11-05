import { Project, Timesheet, TimesheetActivity, User } from "@/_models";
import { TimesheetUpdate } from "@/_models/timsheet-update";
import { AuthenticationService, TimesheetService } from "@/_services";
import { Component, OnInit } from "@angular/core";
import { FormGroup,FormBuilder, Validators, FormArray, FormControl } from "@angular/forms";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";

@Component({ templateUrl: 'update-timesheet.component.html'})

export class UpdateTimesheetComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    timesheetUpdate:TimesheetUpdate=new TimesheetUpdate();
    chooseDate:FormGroup;
    sub:Subscription;
    date:any; 
    users:User[]=[];
    project:number;
    user:number;
    addTimesheetForm:FormGroup;
    submit:boolean;
    myTimesheetUpdate:TimesheetUpdate;

    constructor(
       
        private authenticationService:AuthenticationService,
        private formBuilder: FormBuilder,
        private timsheetService:TimesheetService

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });

    

    }

    ngOnInit()
    {
        //builds form with validators
        this.chooseDate = this.formBuilder.group({
            Date: ['',Validators.required]    
        });

        //init form with validators
        this.addTimesheetForm = this.formBuilder.group({
            Date:['',Validators.required],
            StartTime:['',Validators.required],
            EndTime: ['',Validators.required],
            Break: ['',Validators.required],           
            Location:['',Validators.required],
            TotalWorkedHours:['0 hours 0 minutes'],
            timesheetActivities:this.formBuilder.array([],[Validators.required])   
         });
      
    }
  
    getTimsheetByDate()
    {
        this.timsheetService.getTimsheetByDate(this.chooseDate.controls.Date.value,this.currentUser.idUser).pipe(first()).subscribe(tu=>{ this.timesheetUpdate=tu;
    
             this.addTimesheetForm.controls.Date.setValue(this.timesheetUpdate.Date);

             //console.log(this.timesheetUpdate.timesheetActivities.length);

             for(let i=0;i< this.timesheetUpdate.timesheetActivities.length;i++)
             {
               //init form with validators      
               this.timesheetActivities.push(this.formBuilder.group({
               WorkedHours:this.formBuilder.control('',[Validators.required]),
               Project:this.formBuilder.control('',[Validators.required]),
               Comments:this.formBuilder.control('',[Validators.required]),
               }));
             }  
         });

    }
    //easy access to form array
    get timesheetActivities(): FormArray 
    {
        return this.addTimesheetForm.get('timesheetActivities') as FormArray;
    }
    onSubmit()
    {
        this.submit=true;
        this.getTimsheetByDate();
        
    }
}