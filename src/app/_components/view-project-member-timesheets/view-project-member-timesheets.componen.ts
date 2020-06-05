import { Component, OnInit } from "@angular/core";
import { User, Project } from "@/_models";
import { Subscription } from "rxjs";
import { AuthenticationService, UserService, ProjectService } from "@/_services";
import { first } from "rxjs/operators";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({ templateUrl: 'view-project-member-timesheets.component.html'})

export class ViewProjectMemberTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    users:User[]=[];
    projects:Project[]=[];
    chooseProject:FormGroup;
    submitted=false;
    selectedProject:number=0;

    constructor(private authenticationService:AuthenticationService,private userService:UserService,private projectService:ProjectService,private formBuilder:FormBuilder)
    {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;});
    }

    ngOnInit()
    {      
        this.chooseProject = this.formBuilder.group({
            Project: ['',Validators.required]
        });
      
       this.getAllProjectsById(this.currentUser.idUser);
      
    }

    getAllProjectMembers(id:number)
    {
        this.userService.getUsersByProjectId(id).pipe(first()).subscribe(users=>{this.users=users;console.log("Get request succesfully done");})
    }

    getAllProjectsById(id:number)
    {
        this.projectService.getByManagerId(id).pipe(first()).subscribe(projects=>{this.projects=projects;console.log("Get request succesfully done"); console.log(projects);})
    }

    get f() { return this.chooseProject.controls; }

    onSubmit()
    {
        this.submitted=true;

        this.selectedProject=this.f.Project.value;

        this.getAllProjectMembers(this.selectedProject);
        
    }
}