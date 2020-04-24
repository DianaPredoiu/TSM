import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Project } from '@/_models';

@Injectable({ providedIn: 'root' })
export class ProjectService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Project[]>(`${config.apiUrl}/projects`);
    }

    
}