import { Component, OnInit} from '@angular/core';
import { first } from 'rxjs/operators';
import { Hobby } from '@/_models';
import {HobbyService } from '@/_services';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';


@Component({ templateUrl: 'hobby-data.component.html',  styleUrls: ['./hobby-data.component.css'] })

export class HobbyDataComponent implements OnInit{

    currentHobby: Hobby;
    hobbies: Hobby[]=[];
    show1:boolean=false;
    show:boolean=false;
    updateForm: FormGroup;
    hobby:Hobby;
    createForm: FormGroup;

    constructor(private hobbyService:HobbyService, private formBuilder: FormBuilder) {} 

    ngOnInit() {
       this.getAllHobbies();
       this.updateForm= this.formBuilder.group({
        hobbyName: ['', Validators.required],
        hobbyType: ['', Validators.required]
        
    });

    this.createForm= this.formBuilder.group({
        hobbyName: ['', Validators.required],
        hobbyType: ['', Validators.required]
    });
      
    }

    getAllHobbies(){

        this.hobbyService.getAll().pipe(first()).subscribe(hobbies=> {this.hobbies = hobbies});
    }

    getCurrentHobby(hobby:Hobby){

        this.currentHobby=hobby;
        console.log(this.currentHobby);
    }

    deleteHobby(id:number){

        this.hobbyService.delete(id).pipe(first()).subscribe(()=>this.getAllHobbies());;
    }

    onClick(){
        this.show=true;
    }

    onClick1(){
        this.show1=true;
    }

    onSubmit(){

        this.hobbyService.update(this.updateForm.value,this.currentHobby.idHobby).pipe(first()).subscribe(()=>this.getAllHobbies());
        this.show=false;
    }

    onSubmit1(){

        this.hobbyService.add(this.createForm.value).pipe(first()).subscribe(()=>this.getAllHobbies());
        this.show1=false;
    }
}