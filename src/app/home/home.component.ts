import { Component, OnDestroy} from '@angular/core';
import { Subscription} from 'rxjs';
import { User } from '@/_models';
import {AuthenticationService} from '@/_services';
import { Router } from '@angular/router';

@Component({ templateUrl: 'home.component.html' })

export class HomeComponent implements OnDestroy {
    currentUser: User;
    currentUserSubscription: Subscription;
    users: User[] = [];

    constructor(
        private authenticationService: AuthenticationService,
        private router: Router
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
    }

    ngOnDestroy() {
        // unsubscribe to ensure no memory leaks
        this.currentUserSubscription.unsubscribe();
    }

   
}