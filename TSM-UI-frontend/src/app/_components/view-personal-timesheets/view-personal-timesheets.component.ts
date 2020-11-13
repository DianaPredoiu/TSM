import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService,ProjectAssignmentsService, UserService } from "@/_services";
import { User, Project } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { TimesheetView } from "@/_models/timesheet-view";
import { ActivatedRoute } from "@angular/router";
import { TimesheetObj } from "@/_models/timesheet-obj";

@Component({ templateUrl: 'view-personal-timesheets.component.html',styleUrls: ['table-style.css']})

export class ViewPersonalTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    timesheets:TimesheetView[]=[];
    chooseDate:FormGroup;
    sub:Subscription;
    date:any;
    projects:Project[]=[];
    users:User[]=[];
    // project:number;
    // user:number;
    timesheetObj:TimesheetObj=new TimesheetObj();
    isSelected:boolean=true;
   

    constructor(
       
        private authenticationService:AuthenticationService,
        private projectService:ProjectAssignmentsService,
        private timesheetService:TimesheetService,
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private userService:UserService,
        private projServ:ProjectService

    )
    {this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });

    
   
    }

    ngOnInit()
    {

        if(this.currentUser.idRole==2)
        {
           //gets projects for the current user
            this.getProjectsById();
            this.getTeamMembers();
        }
        if(this.currentUser.idRole==3)
        {
            this.getManagerProjects();
            this.getProjectMembers();
        }
        
        //builds form with validators
    this.chooseDate = this.formBuilder.group({
        Date: ['',Validators.required],
        Project: ['',Validators.required],
        User:['',Validators.required]        
    });

        
    }

    getProjectsById()
    {
        this.projectService.getAll(this.currentUser.idUser).pipe(first()).subscribe(projects=>{this.projects=projects;console.log("get req done");});

    }

    getTeamMembers()
    {
        this.userService.getUsersByTeamId(this.currentUser.idTeam).pipe(first()).subscribe(users=>{this.users=users;console.log("get req done");});
    }

    getProjectMembers()
    {
        this.userService.getUsersByProjectId(this.currentUser.idUser).pipe(first()).subscribe(users=>{this.users=users;console.log("get req done");});
    }

    getManagerProjects()
    {
        this.projServ.getByManagerId(this.currentUser.idUser).pipe(first()).subscribe(projects=>{this.projects=projects;console.log("get req done");});
    }

    // convenience getter for easy access to form fields
    get f() { return this.chooseDate.controls; }


    getByFilter()
    {
        
        if(this.f.Project.value!='AllProjects')
        {
            this.timesheetObj.IdProject=this.projects.find(p=>p.projectName==this.f.Project.value).idProject;
        }
        else
        {
            this.timesheetObj.IdProject=-1;
        }

        if(this.f.User.value!='AllUsers')
        {
            this.timesheetObj.IdUser=this.users.find(p=>p.username==this.f.User.value).idUser;
        }
        else
        {
            this.timesheetObj.IdUser=-1;
        }

        if(this.f.Date.value!='')
        {
             this.timesheetObj.Date=this.f.Date.value;
        }
        else
        {
            this.timesheetObj.Date="nullDate";
        }
        

        if(this.currentUser.idRole==2)
        {
            this.timesheetObj.IdManager=-1;
            if(this.timesheetObj.IdUser==-1)
            {
               this.timesheetObj.IdTeam=this.currentUser.idTeam;
            }
            else
            {
                this.timesheetObj.IdTeam=-1;
            }
        }

        if(this.currentUser.idRole==3)
        {
            this.timesheetObj.IdTeam=-1;
            if(this.timesheetObj.IdProject=-1)
            {
                this.timesheetObj.IdManager=this.currentUser.idUser;
            }
            else
            {
                this.timesheetObj.IdManager=-1;
            }
        }

        if(this.currentUser.idRole==1)
        {
            this.timesheetObj.IdTeam=-1;
            this.timesheetObj.IdManager=-1;
        }
        
        //console.log(this.timesheetObj);
        this.timesheetService.getByGeneratedFilter(this.timesheetObj).pipe(first()).subscribe(timesheets=>{
                    this.timesheets=timesheets;
                    //console.log(timesheets);
                    console.log("GET request succesfully done");
                
                    });

                    console.log(this.timesheets);
    }
 

    onSubmit()
    {
       //this.isSelected=false;
       this.getByFilter();                
    }
}