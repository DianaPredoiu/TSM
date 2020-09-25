import { Component, OnInit } from "@angular/core";
import { User, Project } from "@/_models";
import { Subscription } from "rxjs";
import { UserService, AuthenticationService, ProjectAssignmentsService, ProjectService } from "@/_services";
import { first } from "rxjs/operators";
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";

@Component({ templateUrl: 'view-team-member-timesheets.component.html'})

export class ViewTeamMemberTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    users:User[]=[];
    chooseProject:FormGroup;
    projects:Project[]=[];
    formBuilder:FormBuilder;

    constructor(private authenticationService:AuthenticationService,
                private userService:UserService,
                private projectService:ProjectAssignmentsService,
                private projectServ:ProjectService
                )
    {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;});
    }

    ngOnInit()
    {
        //gets all team members
       this.getAllTeamMembers(this.currentUser.idTeam);
       this.getProjectsById();

       
        //builds form with validators
        this.chooseProject =new FormGroup({
            Date:new FormControl ('',Validators.required),
            Project: new FormControl('',Validators.required),
            Users:new FormControl('',Validators.required)
        });
 
    }

    getProjectsById()
    {               
        this.projectServ.getAll().pipe(first()).subscribe(projects=>{this.projects=projects;console.log("get req done");});
    }

    getAllTeamMembers(id:number)
    {
        this.userService.getUsersByTeamId(id).pipe(first()).subscribe(users=>{this.users=users;console.log("Get request succesfully done");})
    }
}