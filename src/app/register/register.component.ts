import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AlertService, UserService, TeamService,RoleService, AuthenticationService } from '@/_services';
import { User,Team,Role} from '@/_models';

@Component({templateUrl: 'register.component.html'})
export class RegisterComponent implements OnInit {

    registerForm: FormGroup;
    loading = false;
    submitted = false;
    invalidPassword:boolean;
    user:User=new User();
    teams:Team[]=[];
    roles:Role[]=[];

    constructor(
        private formBuilder: FormBuilder,
        private teamService:TeamService,
        private roleService:RoleService,
        private authenticationService:AuthenticationService
    ) { }

    ngOnInit() {

        //buildin form with validators
        this.registerForm = this.formBuilder.group({
            Username: ['', Validators.required],
            Password: ['', Validators.required],
            ConfirmPassword: ['', Validators.required],
            Email: ['', Validators.required],
            Role:['', Validators.required],
            Team:['', Validators.required]
        });

        this.getAllTeams();
        this.getAllRoles();
        
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    //gets the list of all teams
    getAllTeams()
    {
        this.teamService.getAll().pipe(first()).subscribe(teams=>{this.teams=teams; console.log("GET request succesfully done");});
    }

    //gets list of all roles
    getAllRoles()
    {
        this.roleService.getAll().pipe(first()).subscribe(roles=>{this.roles=roles; console.log("GET request succesfully done");});
    }

    onSubmit() {

        this.submitted=true;
        
        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }

        //stop here if passwords don't match
        if(this.f.Password.value==this.f.ConfirmPassword.value)
        {
            //put data into user
            this.user.username=this.f.Username.value;
            this.user.password=this.f.Password.value;
            this.user.email=this.f.Email.value;
            this.user.IdRole=this.f.Role.value;
            this.user.IdTeam=this.f.Team.value;
    
            //registration
            this.authenticationService.register(this.user);
                
        }  
        else
        {
            this.invalidPassword=true;
        }     
    }
}
