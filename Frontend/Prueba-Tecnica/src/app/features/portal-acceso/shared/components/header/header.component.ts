import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { VistaAccesoEnum } from '../../enums/VistaAccesoEnum';

@Component({
  selector: 'app-header',
  imports: [MatIconModule,MatButtonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  @Output() navegar = new EventEmitter<VistaAccesoEnum>();

  @Input({required:true})
  titulo : string = ""

  @Input({required:true})
  descripcion: string = ""

  volver(){
    this.navegar.emit(VistaAccesoEnum.Login); 
  }
}
