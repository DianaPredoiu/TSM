import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService,ProjectAssignmentsService, UserService } from "@/_services";
import { User, Project } from "@/_models";
import { Subscription } from "rxjs";
import { first } from "rxjs/operators";
import { TimesheetView } from "@/_models/timesheet-view";
import { ActivatedRoute } from "@angular/router";
import { IfStmt } from "@angular/compiler";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

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
    project:number;
    user:number;
   

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


    //gets activities by project and date
    getByProjectDateTeamLeader()
    {        
            this.timesheetService.getByProjectDateTeamLeader(this.currentUser.idTeam,this.f.Project.value,this.f.Date.value).pipe(first()).subscribe(timesheets=>{
                this.timesheets=timesheets;
                 console.log("GET request succesfully done");
               
                });
                  
    }

    //gets activities by date 
    getByDateTeamLeader()
    {
       
        this.timesheetService.getByDateTeamLeader(this.currentUser.idTeam,this.f.Date.value).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
    }

    //gets activities by project
    getByProjectTeamLeader()
    {
        this.timesheetService.getByProjectTeamLeader(this.currentUser.idTeam,this.f.Project.value).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
    }

    //gets activities by user
    getByUserTeamLeader()
    {
        this.timesheetService.getByUserTeamLeader(this.f.User.value).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
    }

     //gets activities by user and date
     getByUserDateTeamLeader()
     {
         this.timesheetService.getByUserDateTeamLeader(this.f.User.value,this.f.Date.value).pipe(first()).subscribe(timesheets=>{
             this.timesheets=timesheets;
             console.log("GET request succesfully done");
         
             });
     }

      //gets activities by user and project
      getByUserProjectTeamLeader()
      {
          
          this.timesheetService.getByUserProjectTeamLeader(this.f.Project.value,this.f.User.value).pipe(first()).subscribe(timesheets=>{
              this.timesheets=timesheets;
              console.log("GET request succesfully done");
          
              });

      }

      //gets activities by user and date and project
      getByUserDateProjectTeamLeader()
      {
          this.timesheetService.getByUserDateProjectTeamLeader(this.f.User.value,this.f.Project.value,this.f.Date.value).pipe(first()).subscribe(timesheets=>{
              this.timesheets=timesheets;
              console.log("GET request succesfully done");
          
              });
      }

      getByProjectDate_Manager()
      {
        this.timesheetService.getByProjectDate_Manager(this.f.Project.value,this.f.Date.value).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
      }

      getByProject_Manager()
      {
        this.timesheetService.getByProject_Manager(this.f.Project.value).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
      }

      getByDate_Manager()
      {
        this.timesheetService. getByDate_Manager(this.f.Date.value,this.currentUser.idUser).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
      }

      getByUser_Manager()
      {
        this.timesheetService. getByUser_Manager(this.f.User.value,this.currentUser.idUser).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
      }

      getByUserDate_Manager()
      {
        this.timesheetService.getByUserDate_Manager(this.f.User.value,this.currentUser.idUser,this.f.Date.value).pipe(first()).subscribe(timesheets=>{
            this.timesheets=timesheets;
            console.log("GET request succesfully done");
        
            });
      }
 

    onSubmit()
    {
           
            //if there is a projects selected
            if(this.f.Project.value && this.f.Date.value && this.f.User.value=='')
            {        
                if(this.currentUser.idRole==2)
                {
                   this.getByProjectDateTeamLeader();
                }
                if(this.currentUser.idRole==3)
                {
                   this.getByProjectDate_Manager();
                }                   
            }
            else if(this.f.Date.value && this.f.User.value=='' && this.f.Project.value=='')
            {   
                if(this.currentUser.idRole==2)
                {
                   this.getByDateTeamLeader();
                }
                if(this.currentUser.idRole==3)
                {
                    this.getByDate_Manager();
                }         
            }
            else if(this.f.Project.value && this.f.User.value=='' && this.f.Date.value=='')
            {
                if(this.currentUser.idRole==2)
                {
                   this.getByProjectTeamLeader();
                }
                if(this.currentUser.idRole==3)
                {
                   this.getByProject_Manager();
                }     
            }
            else if(this.f.User.value && this.f.Project.value=='' && this.f.Date.value=='')
            {
                if(this.currentUser.idRole==2)
                {
                   this.getByUserTeamLeader();
                }
                if(this.currentUser.idRole==3)
                {
                   this.getByUser_Manager();
                }     
            }
            else if(this.f.User.value && this.f.Date.value && this.f.Project.value=='')
            {                  
                if(this.currentUser.idRole==2)
                {
                   this.getByUserDateTeamLeader();
                }
                if(this.currentUser.idRole==3)
                {
                   this.getByUserDate_Manager();
                }     
            }
            else if(this.f.User.value && this.f.Project.value && this.f.Date.value=='')
            {                  
                this.getByUserProjectTeamLeader();
            }
            else if(this.f.User.value && this.f.Project.value && this.f.Date.value)
            {
                this.getByUserDateProjectTeamLeader();
            }
                
    }
}