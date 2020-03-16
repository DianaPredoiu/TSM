import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import{Hobby} from '@/_models';

@Injectable({ providedIn: 'root' })
export class QueryService {

    constructor(private http: HttpClient) { }

    getHobiesList(id: number)  {
        return this.http.get<Hobby[]>(`${config.apiUrl}/UserHobbiesView/listhobbies/${id}`);
    }
}