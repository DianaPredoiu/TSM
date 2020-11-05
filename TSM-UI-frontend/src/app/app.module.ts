import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule }    from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent }  from './app.component';
import { routing }        from './app.routing';
import { AlertComponent} from './_components';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './_components/home';
import { LoginComponent } from './_components/login';
import { RegisterComponent } from './_components/register';
import { ChangePasswordComponent } from './_components/change-password/change-password.component';
import { AddTimesheetComponent } from './_components/add-timesheet/add-timesheet.component';
import { ViewPersonalTimesheetsComponent} from './_components/view-personal-timesheets/view-personal-timesheets.component';
import { ViewProjectMemberTimesheetsComponent } from './_components/view-project-member-timesheets/view-project-member-timesheets.componen';
import { ViewTeamMemberTimesheetsComponent } from './_components/view-team-member-timesheets/view-team-member-timesheets.component';
import { UpdateTimesheetComponent } from './_components/update-timesheet/update-timesheet.component';


@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        routing
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        ChangePasswordComponent,
        AddTimesheetComponent,
        ViewPersonalTimesheetsComponent,ViewProjectMemberTimesheetsComponent,ViewTeamMemberTimesheetsComponent,UpdateTimesheetComponent
    
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
      

        // provider used to create fake backend
        //fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }