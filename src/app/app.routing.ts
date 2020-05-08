import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './_components/home';
import { LoginComponent } from './_components/login';
import { RegisterComponent } from './_components/register';
import { AuthGuard } from './_guards';
import {ChangePasswordComponent} from './_components/change-password/change-password.component';
import { AddTimesheetComponent } from './_components/add-timesheet/add-timesheet.component';
import {ViewPersonalTimesheetsComponent} from './_components/view-personal-timesheets/view-personal-timesheets.component';
import { ViewTeamMemberTimesheetsComponent } from './_components/view-team-member-timesheets/view-team-member-timesheets.component';
import { ViewProjectMemberTimesheetsComponent } from './_components/view-project-member-timesheets/view-project-member-timesheets.componen';


const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'changePassword', component: ChangePasswordComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'addTimesheet', component: AddTimesheetComponent },
    { path: 'viewPersonalTimesheets/:id', component: ViewPersonalTimesheetsComponent }   ,
    { path: 'viewTeamMmemberTimesheets', component: ViewTeamMemberTimesheetsComponent },  
    { path: 'viewProjectMemberTimesheets', component: ViewProjectMemberTimesheetsComponent } , 

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);