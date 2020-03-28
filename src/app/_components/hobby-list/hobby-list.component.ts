import { Component, OnInit} from '@angular/core';
import { Subscription} from 'rxjs';
import { first } from 'rxjs/operators';
import { User, Hobby } from '@/_models';
import { AuthenticationService,HobbyService} from '@/_services';


@Component({ templateUrl: 'hobby-list.component.html' })

export class HobbyListComponent implements OnInit{

    currentUser: User;
    currentUserSubscription: Subscription;
    users: User[] = [];
    hobbies: Hobby[]=[];

    constructor(
        private authenticationService: AuthenticationService,
        private hobbyService:HobbyService
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
        this.hobbyService.getHobiesList(this.currentUser).pipe(first()).subscribe(hobbies=> {this.hobbies = hobbies});
       
    }
}