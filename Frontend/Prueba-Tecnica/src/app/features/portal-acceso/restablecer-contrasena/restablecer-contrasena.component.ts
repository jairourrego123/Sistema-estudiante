import { Component, EventEmitter, inject, Output } from '@angular/core';
import { VistaAccesoEnum } from '../shared/enums/VistaAccesoEnum';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field'
import { NgIf} from '@angular/common';
import { HeaderComponent } from "../shared/components/header/header.component";
import { LayoutComponent } from "../shared/components/layout/layout.component";
import { AuthService } from '../../../core/adapters/auth.service';
import { EmailValidator } from '../../../core/validators/emailvalidator';
import { SwalService } from '../../../core/adapters/alert.service';

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

  constructor( private swalService: SwalService ,private fb: FormBuilder, private authService: AuthService) {
    this.form = this.fb.group({
      email: ['', [Validators.required,EmailValidator.validEmail()]]
    });
  }
  

  volver() {
    this.navegar.emit(VistaAccesoEnum.Login); 
  }

  
  enviarCorreo(){
    const { email } = this.form.value;
    this.authService.generarEnlaceRestablecimiento(email).subscribe({
      next: () =>  {
        this.form.reset()
        this.swalService.showAlert({
          icon: 'success',
          title:'!Enviado!',
          text:'Revisa la bandeja de tu correo electronico e ingresa al enlace que te enviamos.',
        })
        return this.volver();
      },
      error: err => this.swalService.showError(err.error.message)
  })
  }
  
}
