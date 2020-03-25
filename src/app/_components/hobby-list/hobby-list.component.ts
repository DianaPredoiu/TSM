import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription} from 'rxjs';
import { first } from 'rxjs/operators';
import { User, Hobby } from '@/_models';
import { UserService, AuthenticationService,QueryService } from '@/_services';


@Component({ templateUrl: 'hobby-list.component.html' })

export class HobbyListComponent implements OnInit{

    currentUser: User;
    currentUserSubscription: Subscription;
    users: User[] = [];
    hobbies: Hobby[]=[];

    constructor(
        private authenticationService: AuthenticationService,
        private queryService:QueryService
    ) 
    {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
    }

    ngOnInit() {
        this.listAllHobbies();
    }

    private listAllHobbies(){   

        console.log(this.currentUser.id);
        this.queryService.getHobiesList(this.currentUser).pipe(first()).subscribe(hobbies=> {this.hobbies = hobbies});
       
    }
}