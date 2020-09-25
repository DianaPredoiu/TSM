import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Project } from '@/_models';

@Injectable({ providedIn: 'root' })
export class ProjectService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Project[]>(`${config.apiUrl}/projects`);
    }

    getByManagerId(id:number)
    {
        //console.log(this.http.get<Project[]>(`${config.apiUrl}/ProjectManager/getProjectsByProjectManagerId/${id}`));
        return this.http.get<Project[]>(`${config.apiUrl}/ProjectManager/getProjectsByProjectManagerId/${id}`);
    }

    getByUserId(id:number) {
        return this.http.get<Project[]>(`${config.apiUrl}/projects/getByUserId/${id}`);
    }
}