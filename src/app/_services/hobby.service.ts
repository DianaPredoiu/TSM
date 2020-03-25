import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hobby } from '@/_models';


@Injectable({ providedIn: 'root' })
export class HobbyService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Hobby[]>(`${config.apiUrl}/hobby`);
    }

    getById(id: number) {
        return this.http.get(`${config.apiUrl}/hobby/${id}`);
    }

    update(hobby: Hobby,id:number) {

      hobby.idHobby=id;
      return this.http.put(`${config.apiUrl}/hobby/${hobby.idHobby}`, hobby);
     }

    delete(id: number) {
        return this.http.delete(`${config.apiUrl}/hobby/${id}`);
    }

    add(hobby:Hobby) {
        return this.http.post(`${config.apiUrl}/hobby/add`,hobby);
    }
}