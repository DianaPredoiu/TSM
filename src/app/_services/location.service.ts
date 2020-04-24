import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Location } from '@/_models';

@Injectable({ providedIn: 'root' })
export class LocationService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Location[]>(`${config.apiUrl}/location`);
    }

    
}