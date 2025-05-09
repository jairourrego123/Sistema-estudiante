import { Component, EventEmitter, Output } from '@angular/core';
import { RegistroComponent } from "./registro/registro.component";
import { LoginComponent } from "./login/login.component";
import { VistaAccesoEnum } from '../shared/enums/VistaAccesoEnum';
import { MatButtonModule } from '@angular/material/button';
import { LayoutComponent } from "../shared/components/layout/layout.component";

@Component({
  selector: 'app-overlay-auth',
  imports: [RegistroComponent, LoginComponent, MatButtonModule, LayoutComponent],
  templateUrl: './overlay-auth.component.html',
  styleUrl: './overlay-auth.component.css'
})
export class OverlayAuthComponent {

  @Output() navegar = new EventEmitter<VistaAccesoEnum>();

 isRightPanelActive = false;

  togglePanel(active: boolean) {
    this.isRightPanelActive = active;
  }

  cambiarVista(vista: VistaAccesoEnum) {    this.navegar.emit(vista);
  }
}
