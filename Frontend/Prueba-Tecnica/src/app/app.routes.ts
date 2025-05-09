import { Routes } from '@angular/router';
import { PortalAccesoComponent } from './features/portal-acceso/portal-acceso.component';
import { VistaAccesoEnum } from './features/portal-acceso/shared/enums/VistaAccesoEnum';

export const routes: Routes =  [
  {path:'portal-acceso', component: PortalAccesoComponent,data: { vista: VistaAccesoEnum.Login }},
  {
    path: 'restablecer-contrasena',   
    component:PortalAccesoComponent,
    data: { vista: VistaAccesoEnum.NuevaClave },
  },
  { path: '**', redirectTo: 'portal-acceso' }

];