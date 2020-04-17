import { Component, OnInit} from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthenticationService, AlertService, UserService} from '@/_services';
import { first} from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '@/_models';


@Component({ templateUrl: 'change-password.component.html' })

export class ChangePasswordComponent implements OnInit{

    currentUser: User;
    currentUserSubscription: Subscription;
    user:User=new User();

    changePasswordForm: FormGroup;
    loading = false;

    validNewPassword:boolean;//for ts part
    validPassword:boolean;//for ts part   
    invalidPassword:boolean;//for html part
    invalidNewPassword:boolean;//for html part
    invalidPass:boolean;//check if the old pass is the same with the new pass

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService,
        private userService:UserService,
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
       
      }

    //initializing component
    ngOnInit() {

        //initializing form
        this.changePasswordForm = this.formBuilder.group({
            currentPass: ['', Validators.required],
            newPassword: ['', Validators.required],
            confirmNewPassword: ['', Validators.required]
        });
    }

    //getter for more simple control work
    get f() { return this.changePasswordForm.controls; }


    //verifing paswword trough a post request 
    verifyPassword(user:User)
    {
        this.userService.verifyPassword(user).subscribe(
            ()       => {this.validPassword=true;this.invalidPassword=false;
                     console.log("POST request succesfully done");},
            (error)  =>{this.validPassword=false;this.invalidPassword=true;
                console.log(this.validPassword)
                console.log("POST request error:", error);}
                );
    }

    //mathcing password function for simplicity of code
    verifyPasswordMatch(p1:string,p2:string)
    {
        if(p1===p2)       
            {this.validNewPassword=true;this.invalidNewPassword=false;}
        else
           {this.validNewPassword=false;this.invalidNewPassword=true;} 
    }

    //form submition
    onSubmit() {

        //put password and id into a user object
        this.user.idUser=this.currentUser.idUser;
        this.user.password=this.f.currentPass.value;
        
        //verify if password is correct and new passwords match
        this.verifyPassword(this.user);
        this.verifyPasswordMatch(this.f.newPassword.value,this.f.confirmNewPassword.value);
     
        //validation 
        if(this.validPassword && this.validNewPassword )
        {    
            //this.loading = true;
            //finally verify if current password is different from new password
            if(this.f.currentPass.value!=this.f.newPassword.value)
            {
                
                this.currentUser.password=this.f.newPassword.value;
               
                this.userService.update(this.currentUser).pipe(first()).subscribe(()=>
                                                                        {console.log("UPDATE request succefully done");this.router.navigate(['/login']);},
                                                                         error => {this.alertService.error(error);this.loading = false;});               
            }
            else                        
               {this.invalidPass=true;console.log(this.invalidPass);} 
            
        }
        
    }

   
}