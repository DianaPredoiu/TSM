import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User_Hobby } from '@/_models';

@Injectable({ providedIn: 'root' })
export class UserHobbyService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User_Hobby[]>(`${config.apiUrl}/user_hobby`);
    }

    getById(id: number) {
        return this.http.get(`${config.apiUrl}/user_hobby/${id}`);
    }

    update(user_hobby: User_Hobby) {
        return this.http.put(`${config.apiUrl}/user_hobby/${user_hobby.id}`, user_hobby);
    }

    delete(id: number) {
        console.log(id);
        return this.http.delete(`${config.apiUrl}/user_hobby/${id}`).subscribe(
            data  => {
            console.log("DELETE Request is successful ", data);
            },
            error  => {
            
            console.log("Error", error);
            
            });
            
    }

    add(user_hobby: User_Hobby){ 
        return this.http.post(`${config.apiUrl}/user_hobby/add`,user_hobby).subscribe(
            data  => {
            console.log("POST Request is successful ", data);
            },
            error  => {
            
            console.log("Error", error);
            
            }
            
            );
            
    }
}