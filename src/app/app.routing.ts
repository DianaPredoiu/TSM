import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { AuthGuard } from './_guards';
import { HobbyListComponent } from './_components/hobby-list/hobby-list.component';
import { AddHobbyComponent } from './_components/add-hobby/add-hobby.component';
import { DeleteHobbyComponent } from './_components/delete-hobby/delete-hobby.component';
import {HobbyDataComponent} from './_components/hobby-data-admin/hobby-data.component';
import {UserDataComponent} from './_components/user-data-admin/user-data.component';


const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'viewHobbies', component: HobbyListComponent },
    { path: 'addHobby', component: AddHobbyComponent },
    { path: 'deleteHobby', component: DeleteHobbyComponent },
    { path: 'hobbyData', component:HobbyDataComponent},
    { path: 'userData', component: UserDataComponent },
   
        

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);