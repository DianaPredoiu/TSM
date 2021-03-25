import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, ProjectService,LocationService,TimesheetService,TimesheetActivityService,AlertService,ProjectAssignmentsService, UserService } from "@/_services";
import { User, Project } from "@/_models";
import { Observable, Subject, Subscription } from "rxjs";
import { first, map, share, tap } from "rxjs/operators";
import { TimesheetView } from "@/_models/timesheet-view";
import { ActivatedRoute,Router } from "@angular/router";
import { TimesheetObj } from "@/_models/timesheet-obj";
import { htmlAstToRender3Ast } from "@angular/compiler/src/render3/r3_template_transform";
import { HttpClient } from "@angular/common/http";
import { resolve } from "url";

@Component({ templateUrl: 'view-personal-timesheets.component.html',styleUrls: ['table-style.css']})

export class ViewPersonalTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    timesheets:TimesheetView[]=new Array<TimesheetView>();
    chooseDate:FormGroup;
    sub:Subscription;
    date:any;
    projects:Project[]=[];
    users:User[]=[];
    // project:number;
    // user:number;
    timesheetObj:TimesheetObj=new TimesheetObj();
    expanded: boolean=false;
    //data:TimesheetView[]=[];
    timesheetsSubscription:Subscription;
    ts:TimesheetView[]=new Array<TimesheetView>();
    

    constructor(
       
        private authenticationService:AuthenticationService,
        private projectService:ProjectAssignmentsService,
        private timesheetService:TimesheetService,
        private formBuilder: FormBuilder,
        private router: Router,
        private userService:UserService,
        private projServ:ProjectService,
        private http: HttpClient

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
        Project: ["All Projects",Validators.required],
        User:[this.currentUser.username,Validators.required]        
    });

        
    }

    // onClick(i:number)
    // {
    //     this.isCollapsed=false;
    //     for(let j=0;j<=this.timesheets.length;j++)
    //     {
    //         if(j==i)
    //         {
    //             this.isCollapsed=true;
    //         }
    //     }
    // }

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
        
        if(this.f.Project.value!='All Projects')
        {
            this.timesheetObj.IdProject=this.projects.find(p=>p.projectName==this.f.Project.value).idProject;
        }
        else
        {
            this.timesheetObj.IdProject=-1;
        }

        if(this.f.User.value!='All Users')
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
        
        //const result: Subject<Array<TimesheetView>> = new Subject<Array<TimesheetView>>();
   
        //console.log(this.timesheetObj);
        // this.timesheetService.getByGeneratedFilter(this.timesheetObj).subscribe(timesheets=>{
        //    this.timesheets=timesheets;
          
        // });
        let promise=new Promise((resolve,reject)=>{
            this.timesheetService.getByGeneratedFilter(this.timesheetObj).subscribe(data=>
                this.timesheets=data);

                if(this.timesheets!=[])
                {
                   resolve("success");
                }
                else
                {
                    reject("error");
                }
        });

        promise.then((message)=>console.log(message));
                  
    }
 

    onSubmit()
    {
       //this.isSelected=false;
       this.getByFilter();

       

       console.log(this.timesheets);
       
    }

    // editActivity(timesheet:TimesheetView)
    // {
    //     timesheet=new TimesheetView();
    //     this.timesheetService.data = timesheet;
    //     this.router.navigate(['/addTimesheet']);
    // }

    
}