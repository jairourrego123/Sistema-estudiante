import { Routes } from '@angular/router';
import { LoginComponent } from './features/portal-acceso/overlay-auth/login/login.component';
import { HomeComponent } from './features/home/home.component';
import { AuthGuard } from './core/guards/auth.guard';
import { RegistroComponent } from './features/portal-acceso/overlay-auth/registro/registro.component';
import { PortalAccesoComponent } from './features/portal-acceso/portal-acceso.component';
import { OverlayAuthComponent } from './features/portal-acceso/overlay-auth/overlay-auth.component';

export const routes: Routes =  [
  {path:'portal-acceso', component: PortalAccesoComponent},
  { path: '**', redirectTo: 'portal-acceso' }

];