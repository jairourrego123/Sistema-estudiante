import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { HomeComponent } from './features/home/home.component';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { 
    path: 'home', 
    component: HomeComponent,
    canActivate: [AuthGuard] 
  }
];