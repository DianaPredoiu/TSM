import { Component, OnDestroy, OnInit} from '@angular/core';
import { Subscription} from 'rxjs';
import { User } from '@/_models';
import {AuthenticationService} from '@/_services';
import { Router } from '@angular/router';
import { FormGroup,FormBuilder, Validators} from '@angular/forms';

@Component({ templateUrl: 'home.component.html' })

export class HomeComponent implements OnDestroy,OnInit {
    currentUser: User;
    currentUserSubscription: Subscription;
    users: User[] = [];
    dropdownForm: FormGroup;

    constructor(
        private authenticationService: AuthenticationService,
        private router: Router,
        private formBuilder: FormBuilder,
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });

        //console.log(document.getElementById("home-dropdown"));
    }

    ngOnInit()
    {
        this.dropdownForm = this.formBuilder.group({
            Options:  ['', Validators.required]
        });
    }

    ngOnDestroy() {
        // unsubscribe to ensure no memory leaks
        this.currentUserSubscription.unsubscribe();
    }

     // convenience getter for easy access to form fields
    get f() { return this.dropdownForm.controls; }

    onSubmit()
    {
        if(this.f.Options.value=="1")
           {this.router.navigate(['/addTimesheet']);}
        if(this.f.Options.value=="2")
        {
            this.router.navigate(['/viewAllTimesheets']);
        }
    }
}