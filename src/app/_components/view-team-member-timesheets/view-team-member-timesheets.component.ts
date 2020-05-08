import { Component, OnInit } from "@angular/core";
import { User } from "@/_models";
import { Subscription } from "rxjs";
import { UserService, AuthenticationService } from "@/_services";
import { first } from "rxjs/operators";
import { FormGroup } from "@angular/forms";

@Component({ templateUrl: 'view-team-member-timesheets.component.html'})

export class ViewTeamMemberTimesheetsComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    users:User[]=[];
    chooseProject:FormGroup;

    constructor(private authenticationService:AuthenticationService,private userService:UserService)
    {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;});
    }

    ngOnInit()
    {
       this.getAllTeamMembers(this.currentUser.idTeam);
       console.log(this.currentUser.idTeam);
    }

    getAllTeamMembers(id:number)
    {
        this.userService.getUsersByTeamId(id).pipe(first()).subscribe(users=>{this.users=users;console.log("Get request succesfully done");})
    }
}