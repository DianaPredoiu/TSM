import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import{Hobby, User} from '@/_models';

@Injectable({ providedIn: 'root' })
export class QueryService {

    constructor(private http: HttpClient) { }

    getHobiesList(user:User)  {
        
        return this.http.get<Hobby[]>(`${config.apiUrl}/UserHobbiesView/listhobbies/${user.id}`);
    }

    getOptionsList(id: number)  {
        return this.http.get<Hobby[]>(`${config.apiUrl}/UserHobbiesView/listoptions/${id}`);
    }
}