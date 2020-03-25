import { Component, OnInit} from '@angular/core';
import {User,Hobby, User_Hobby} from '@/_models';
import { Subscription} from 'rxjs';
import {  AuthenticationService,QueryService,UserHobbyService} from '@/_services';
import { first } from 'rxjs/operators';

@Component({ templateUrl: 'add-hobby.component.html' })

export class AddHobbyComponent implements OnInit{

    currentUser: User;
    currentUserSubscription: Subscription;
    hobbies: Hobby[]=[];

    constructor(
        private authenticationService: AuthenticationService,
        private queryService:QueryService,
        private userHobbyService:UserHobbyService
    ) 
    {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
    }

    ngOnInit() {

       this.loadAllHobbies();
    }

    private loadAllHobbies(){

        this.queryService.getOptionsList(this.currentUser.id).pipe(first()).subscribe(hobbies=>{this.hobbies=hobbies});
    }

    addHobby(idHobby:number){
        
        let user_hobby= new User_Hobby();
        user_hobby.userId=this.currentUser.id;
        user_hobby.hobbyId=idHobby;

        this.userHobbyService.add(user_hobby);
    }
}