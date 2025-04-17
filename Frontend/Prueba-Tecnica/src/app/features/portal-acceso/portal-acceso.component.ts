import { Component } from '@angular/core';
import { VistaAccesoEnum } from './VistaAccesoEnum';
import { OverlayAuthComponent } from "./overlay-auth/overlay-auth.component";
import { NgSwitch, NgSwitchCase, NgSwitchDefault } from '@angular/common';
import { RestablecerContrasenaComponent } from "./restablecer-contrasena/restablecer-contrasena.component";
@Component({
  selector: 'app-portal-acceso',
  imports: [NgSwitchCase, NgSwitch, OverlayAuthComponent, RestablecerContrasenaComponent],
  templateUrl: './portal-acceso.component.html',
  styleUrl: './portal-acceso.component.css'
})
export class PortalAccesoComponent {
  vistaActual: VistaAccesoEnum = VistaAccesoEnum.Login;

  cambiarVista(vista: VistaAccesoEnum) {
    this.vistaActual = vista;
  }

  get VistaAccesoEnum() {
    return VistaAccesoEnum;
  }
}
