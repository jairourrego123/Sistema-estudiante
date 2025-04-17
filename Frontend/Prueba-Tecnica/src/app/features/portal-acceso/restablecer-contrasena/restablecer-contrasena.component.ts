import { Component, EventEmitter, inject, Output } from '@angular/core';
import { VistaAccesoEnum } from '../VistaAccesoEnum';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field'
import { NgIf} from '@angular/common';
@Component({
  selector: 'app-restablecer-contrasena',
  imports: [ NgIf,MatIconModule,MatInputModule,MatButtonModule,ReactiveFormsModule,MatFormFieldModule],
  templateUrl: './restablecer-contrasena.component.html',
  styleUrl: './restablecer-contrasena.component.css'
})
export class RestablecerContrasenaComponent {
  
  @Output() navegar = new EventEmitter<VistaAccesoEnum>();
  
  
  private formbuilder = inject(FormBuilder)
  form = this.formbuilder.group({
    correo: ['', [Validators.required, Validators.email]]
  });
  


  enviarCorreo(){
    console.log("correo enviado");
    
  }

  volver() {
    this.navegar.emit(VistaAccesoEnum.Login); 
  }
  
}
