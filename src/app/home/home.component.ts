import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription} from 'rxjs';
import { first } from 'rxjs/operators';
import { User, Hobby } from '@/_models';
import { UserService, AuthenticationService,QueryService } from '@/_services';


@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit, OnDestroy {
    currentUser: User;
    currentUserSubscription: Subscription;
    users: User[] = [];
    hobbies: Hobby[]=[];


    constructor(
        private authenticationService: AuthenticationService,
        private userService: UserService,
        private queryService:QueryService
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
    }

    ngOnInit() {
        this.listAllHobbies();
    }

    ngOnDestroy() {
        // unsubscribe to ensure no memory leaks
        this.currentUserSubscription.unsubscribe();
    }

    deleteUser(id: number) {
        this.userService.delete(id).pipe(first()).subscribe(() => {
            this.loadAllUsers()
        });
    }

    private loadAllUsers() {
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.users = users;
        });
        
    }

    private listAllHobbies()
    {
        this.queryService.getHobiesList(this.currentUser.id).pipe(first()).subscribe(hobbies=> {this.hobbies = hobbies});
       
    }
}