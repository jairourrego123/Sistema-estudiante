import { Component, EventEmitter, Output } from '@angular/core';
import { RegistroComponent } from "./registro/registro.component";
import { LoginComponent } from "./login/login.component";
import { VistaAccesoEnum } from '../VistaAccesoEnum';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-overlay-auth',
  imports: [RegistroComponent, LoginComponent,MatButtonModule],
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
