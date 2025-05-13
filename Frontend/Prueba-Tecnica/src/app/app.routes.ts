import { Routes } from '@angular/router';
import { PortalAccesoComponent } from './features/portal-acceso/portal-acceso.component';
import { VistaAccesoEnum } from './features/portal-acceso/shared/enums/VistaAccesoEnum';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes =  [
  {path:'portal-acceso', component: PortalAccesoComponent,data: { vista: VistaAccesoEnum.Login }},
  {
    path: 'restablecer-contrasena',   
    component:PortalAccesoComponent,
    data: { vista: VistaAccesoEnum.NuevaClave },
  },
  {
    path: 'home',   
    component:HomeComponent
  },
  {
    path: "",
    redirectTo: "academic/students",
    pathMatch: "full",
  },
  {
    path: "",
    component: HomeComponent,
    // canActivate: [authGuard],
    children: [
      {
        path: "",
        redirectTo: "academic/students",
        pathMatch: "full",
      },
      {
        path: "academic",
        children: [
          {
            path: "students",
            loadComponent: () => import("./features/home/pages/students.component").then((m) => m.StudentsComponent),
          },
          {
            path: "professors",
            loadComponent: () => import("./features/home/pages/professors.component").then((m) => m.ProfessorsComponent),
          },
          {
            path: "subjects",
            loadComponent: () => import("./features/home/pages/subjects.component").then((m) => m.SubjectsComponent),
          },
          {
            path: "registrations",
            loadComponent: () =>
              import("./features/home/pages/registrations.component").then((m) => m.RegistrationsComponent),
          },
        ],
      },
     
    ],
  },
  { path: '**', redirectTo: 'portal-acceso' }
  
];