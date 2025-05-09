import { Component, inject, OnInit } from '@angular/core';
import { OverlayAuthComponent } from "./overlay-auth/overlay-auth.component";
import { NgSwitch, NgSwitchCase, NgSwitchDefault } from '@angular/common';
import { RestablecerContrasenaComponent } from "./restablecer-contrasena/restablecer-contrasena.component";
import { ActivatedRoute, Router } from '@angular/router';
import { NuevaContrasenaComponent } from "./nueva-contrasena/nueva-contrasena.component";
import { LayoutComponent } from "./shared/components/layout/layout.component";
import { HeaderComponent } from "./shared/components/header/header.component";
import { VistaAccesoEnum } from './shared/enums/VistaAccesoEnum';
import { HeaderEnum } from './shared/enums/HeaderEnum';

@Component({
  selector: 'app-portal-acceso',
  imports: [NgSwitchCase, NgSwitch, OverlayAuthComponent, RestablecerContrasenaComponent, NuevaContrasenaComponent],
  templateUrl: './portal-acceso.component.html',
  styleUrl: './portal-acceso.component.css'
})
export class PortalAccesoComponent implements OnInit {
  vistaActual: VistaAccesoEnum = VistaAccesoEnum.Login;
  header:HeaderEnum = HeaderEnum.TituloRestablecerContrasena;

  router = inject(ActivatedRoute);
  
  ngOnInit(): void {
    this.router.data.subscribe(data => {
      console.log(data);
      
      if (data['vista']) {
        this.vistaActual = data['vista'];
      }
    });

  }

  cambiarVista(vista: VistaAccesoEnum) {
    this.vistaActual = vista;
  }

  get VistaAccesoEnum() {
    return VistaAccesoEnum;
  }
}
