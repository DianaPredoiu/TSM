import { Component, OnInit} from '@angular/core';
import {User,Hobby, User_Hobby} from '@/_models';
import { Subscription } from 'rxjs';
import {AuthenticationService,QueryService,UserHobbyService} from '@/_services';
import { first } from 'rxjs/operators';


@Component({ templateUrl: 'delete-hobby.component.html' })

export class DeleteHobbyComponent implements OnInit{

    currentUser: User;
    currentUserSubscription: Subscription;
    hobbies: Hobby[]=[];
    user_hobby:User_Hobby[];

    constructor(
        private authenticationService: AuthenticationService,
        private queryService:QueryService,
        private userHobbyService:UserHobbyService
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
    }

    ngOnInit() {

       this.listAllHobbies();
       this.getAllUserHobby();
     
    }

    private listAllHobbies(){
        
        this.queryService.getHobiesList(this.currentUser).pipe(first()).subscribe(hobbies=> {this.hobbies = hobbies});
       
    }
    getAllUserHobby(){

        this.userHobbyService.getAll().pipe(first()).subscribe(user_hobby=>this.user_hobby=user_hobby);
    }
    
    deleteHobby(idHobby:number){

        for (let item of this.user_hobby)

          if(item.userId==this.currentUser.id && item.hobbyId==idHobby)
             this.userHobbyService.delete(item.id);
    }

}