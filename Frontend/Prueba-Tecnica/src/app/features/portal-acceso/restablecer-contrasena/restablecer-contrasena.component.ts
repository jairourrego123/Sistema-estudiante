import { Component, EventEmitter, inject, Output } from '@angular/core';
import { VistaAccesoEnum } from '../shared/enums/VistaAccesoEnum';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field'
import { NgIf} from '@angular/common';
import { HeaderComponent } from "../shared/components/header/header.component";
import { LayoutComponent } from "../shared/components/layout/layout.component";
import { AuthService } from '../../../core/adapters/auth.service';
@Component({
  selector: 'app-restablecer-contrasena',
  imports: [NgIf, MatIconModule, MatInputModule, MatButtonModule, ReactiveFormsModule, MatFormFieldModule, HeaderComponent, LayoutComponent],
  templateUrl: './restablecer-contrasena.component.html',
  styleUrl: './restablecer-contrasena.component.css'
})
export class RestablecerContrasenaComponent {
  
  @Output() navegar = new EventEmitter<VistaAccesoEnum>();
  
  titulo = "Recuperar contraseña"
  descripcion = "Ingresa tu correo electrónico para enviarte un enlace de recuperación."
  form: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.form = this.fb.group({
      email: ['', [Validators.required,Validators.email]]
    });
  }
  

  volver() {
    this.navegar.emit(VistaAccesoEnum.Login); 
  }

  
  enviarCorreo(){
    const { correo } = this.form.value;
    this.authService.generarEnlaceRestablecimiento(correo).subscribe({
      next: () =>  this.volver(),
      error: (error) => console.error('Error de autenticación:', error)
    });
  }
  
}
