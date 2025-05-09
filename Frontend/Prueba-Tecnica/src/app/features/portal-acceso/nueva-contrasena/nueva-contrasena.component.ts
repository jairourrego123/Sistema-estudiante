import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from "../shared/components/header/header.component";
import { VistaAccesoEnum } from '../shared/enums/VistaAccesoEnum';
import { LayoutComponent } from "../shared/components/layout/layout.component";
import { PasswordInputComponent } from "../shared/components/password-input/password-input.component";
import { AuthService } from '../../../core/adapters/auth.service';

@Component({
  selector: 'app-nueva-contrasena',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    HeaderComponent,
    LayoutComponent,
    PasswordInputComponent
],
  templateUrl: './nueva-contrasena.component.html',
  styleUrls: ['./nueva-contrasena.component.css']
})
export class NuevaContrasenaComponent implements OnInit {

  @Output() navegar = new EventEmitter<VistaAccesoEnum>();


  form: FormGroup;
  isLoading = false;
  hidePassword = true;
  hideConfirmPassword = true;
  titulo = "Cambia tu contraseña"
  descripcion = "Ingresa la nueva contraseña con la que quieres ingresar de ahora en adelante."

  private _token = '';
  private _email = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    private authService : AuthService
  ) {
    this.form = this.formulario();
  }

  ngOnInit(): void {
    this.obtenerParametrosUrl();
    if (!this.validarParametrosUrl()) {
      this.handleUrlInvalida();
      return;
    }
    this.limpiarURL();
  }

  visualizarClave(field: string): void {
    if (field === 'nuevaClave') {
      this.hidePassword = !this.hidePassword;
    } else if (field === 'confirmarClave') {
      this.hideConfirmPassword = !this.hideConfirmPassword;
    }
  }


  private obtenerParametrosUrl(): void {
    const params = this.route.snapshot.queryParams;
    this._token = params['token'] || '';
    this._email = params['email'] || '';
  }


  private validarParametrosUrl(): boolean {
    return !!(this._token && this._email);
  }


  private handleUrlInvalida(): void {
    this.mostrarError('Enlace inválido o expirado');
    this.router.navigate(['/']);
  }


  private limpiarURL(): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {},
      replaceUrl: true
    });
  }

  private formulario(): FormGroup {
    const formGroup = this.fb.group({
      nuevaClave: ['', [Validators.required, Validators.minLength(8)]],
      confirmarClave: ['', Validators.required]
    });
    
    formGroup.setValidators(this.validarMatchClaves);
    return formGroup;
  }

  
  private validarMatchClaves(group: AbstractControl): null | { noCoincide: boolean } {
    const formGroup = group as FormGroup;
    const clave = formGroup.get('nuevaClave')?.value;
    const confirmar = formGroup.get('confirmarClave')?.value;
    
    return clave === confirmar ? null : { noCoincide: true };
  }

  cambiarClave(): void {
    if (this.form.invalid) return;

    this.isLoading = true;
    const nuevaClave = this.form.value.nuevaClave;
    console.log('Datos que se enviarían:', {
      token: this._token,
      email: this._email,
      nuevaClave
    });
    
    this.authService.restablecerContraseña(this._email,this._token,nuevaClave).subscribe(
      {
        next: () =>  this.manejarExito(),
        error: (error) => this.manejarError(error)
      });
    

   
  }
  private manejarExito(): void {
    this.isLoading = false;
    this.mostrarExito('Contraseña cambiada con éxito');
    this.router.navigate(['/login']);
  }


  private manejarError(error: any): void {
    this.isLoading = false;
    const mensaje = error.error?.message || 'Error al cambiar la contraseña';
    this.mostrarError(mensaje);
  }

  private mostrarError(mensaje: string): void {
    this.snackBar.open(mensaje, 'Cerrar', {
      duration: 5000,
      panelClass: ['error-snackbar']
    });
  }


  private mostrarExito(mensaje: string): void {
    this.snackBar.open(mensaje, 'Cerrar', {
      duration: 3000,
      panelClass: ['success-snackbar']
    });
  }

  volver(){
    this.navegar.emit(VistaAccesoEnum.Login); 
  }
}