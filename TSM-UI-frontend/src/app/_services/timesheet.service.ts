import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Timesheet, TimesheetView } from '@/_models';
import {AlertService} from './alert.service';
import {Router} from '@angular/router';
import { TimesheetUpdate } from '@/_models/timsheet-update';

@Injectable({ providedIn: 'root' })
export class TimesheetService {

    constructor(private http: HttpClient,private alertService:AlertService,private router:Router) { }

    add(timesheet: Timesheet) {
        return this.http.post(`${config.apiUrl}/timesheets/create`, timesheet);        
    }

    //gets timesheet by date for team leader 1
    getByDateTeamLeader(idTeam:number,date:string)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byDateTeamLeader/${idTeam}/${date}`);
    }

    //gets timesheet by project for team leader 2
    getByProjectTeamLeader(idTeam:number,idProj:number)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byProjectTeamLeader/${idTeam}/${idProj}`);
    }

    //gets timesheet by date and project for team leader 3
    getByProjectDateTeamLeader(idTeam:number,idProj:number,date:string)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byProjectDateTeamLeader/${idProj}/${idTeam}/${date}`);
    }
    //gets timesheet by user for team leader 4
    getByUserTeamLeader(IdUser:number)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byUserTeamLeader/${IdUser}`);
    }

    //gets timesheet by user and date for team leader 5
    getByUserDateTeamLeader(IdUser:number,date:string)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byUserDateTeamLeader/${IdUser}/${date}`);
    }

    //gets timesheet by user and project for team leader and project manager 6
    getByUserProjectTeamLeader(IdProject:number,IdUser:number)
    {
       
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byProjectUserTeamLeader/${IdProject}/${IdUser}`);
    }

    //gets timesheet by user date and project for team leader and project manager 7
    getByUserDateProjectTeamLeader(IdUser:number,IdProject:number,date:string)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byProjectUserDateTeamLeader/${IdProject}/${IdUser}/${date}`);
    }

    getByProjectDate_Manager(IdProject:number,date:string)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byProjectDate_ProjectManager/${IdProject}/${date}`);
    }

    getByProject_Manager(IdProject:number)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byProject_ProjectManager/${IdProject}`);
    }

    getByDate_Manager(date:string,IdManager:number)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byDate_ProjectManager/${date}/${IdManager}`);
    }

    getByUser_Manager(IdUser:number,IdManager:number)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byUser_projectManager/${IdManager}/${IdUser}`);
    }

    getByUserDate_Manager(IdUser:string,IdManager:number,date:string)
    {
        return this.http.get<TimesheetView[]>(`${config.apiUrl}/timesheets/byUserDate_ProjectManager/${IdManager}/${IdUser}/${date}`);
    }

    getTimsheetByDate(date:string,IdUser:number)
    {
        return this.http.get<TimesheetUpdate>(`${config.apiUrl}/timesheets/byDate/${date}/${IdUser}`);
    }

}