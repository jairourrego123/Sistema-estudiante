import { Component, EventEmitter, input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/adapters/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgIf } from '@angular/common';
import { VistaAccesoEnum } from '../../VistaAccesoEnum';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, MatIconModule,MatInputModule, MatButtonModule,NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  @Output() navegar = new EventEmitter<VistaAccesoEnum>();


  form: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  

login(): void {
    const { username, password } = this.form.value;
    this.authService.login(username, password).subscribe({
      next: () =>  this.router.navigate(['/home']),
      error: (error) => console.error('Error de autenticación:', error)
    });
}

irARecuperarClave() {
  this.navegar.emit(VistaAccesoEnum.ForgotPassword);
}
}
