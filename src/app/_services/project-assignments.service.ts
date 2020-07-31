import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Project } from '@/_models/project';

@Injectable({ providedIn: 'root' })
export class ProjectAssignmentsService {

    constructor(private http: HttpClient) { }

    getAll(id:number) {
        return this.http.get<Project[]>(`${config.apiUrl}/projectAssignments/${id}`);
    }
}