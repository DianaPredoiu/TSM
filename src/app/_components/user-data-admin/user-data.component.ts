import { Component, OnInit } from '@angular/core';
import { Subscription} from 'rxjs';
import { first } from 'rxjs/operators';
import { User, Hobby } from '@/_models';
import { UserService} from '@/_services';


@Component({ templateUrl: 'user-data.component.html' ,styleUrls: ['./user-data.component.css'] })

export class UserDataComponent implements OnInit {

    currentUser: User;
    currentUserSubscription: Subscription;
    users: User[] = [];
    hobbies: Hobby[]=[];
    activator:boolean=true;
    deactivator:boolean=false;

    constructor(private userService: UserService) {}

    ngOnInit() {
        this.loadAllUsers();
    }

    private loadAllUsers() {
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.users = users;
        });
        
    }

    activateUser(){

        this.currentUser.isActive=true;
        this.userService.update(this.currentUser).pipe(first()).subscribe(()=>this.loadAllUsers);
        this.deactivator=true;
    }

    deactivateUser(){

        this.currentUser.isActive=false;
        this.userService.update(this.currentUser).pipe(first()).subscribe(()=>this.loadAllUsers);
        this.activator=true;
    }

    getCurrentUser(user:User){
        
        this.currentUser=user;
        console.log( this.currentUser);
    }
}