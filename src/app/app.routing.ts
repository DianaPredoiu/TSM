import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { AuthGuard } from './_guards';
import { HobbyListComponent } from './hobby-list.component';
import { AddHobbyComponent } from './add-hobby.component';
import { DeleteHobbyComponent } from './delete-hobby.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'viewHobbies', component: HobbyListComponent },
    { path: 'addHobby', component: AddHobbyComponent },
    { path: 'deleteHobby', component: DeleteHobbyComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);