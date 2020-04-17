import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Role } from '@/_models';

@Injectable({ providedIn: 'root' })
export class RoleService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Role[]>(`${config.apiUrl}/roles`);
    }

    
}