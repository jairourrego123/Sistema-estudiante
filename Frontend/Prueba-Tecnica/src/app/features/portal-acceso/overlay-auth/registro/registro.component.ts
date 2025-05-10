import { CommonModule } from '@angular/common';
import { Component, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/adapters/auth.service';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { PasswordInputComponent } from "../../shared/components/password-input/password-input.component";
import { PasswordValidator } from '../../../../core/validators/password.validator';
import { EmailValidator } from '../../../../core/validators/emailvalidator';

@Component({
  selector: 'app-registro',
  imports: [CommonModule, MatIconModule, ReactiveFormsModule, MatInputModule, MatButtonModule, MatCardModule, PasswordInputComponent],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PasswordInputComponent),
      multi: true
    }
  ]
})
export class RegistroComponent {

  form: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.form = this.fb.group({
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      email: ['', [Validators.required, EmailValidator.validEmail()]],
      password: ['',[ Validators.required,PasswordValidator.strongPassword()]],
    });
  }

  registrar(): void {
    const user = this.form.value;
    this.authService.register(user).subscribe({
      next: () => this.router.navigate(['/login']),
      error: (error) => console.error('Error en el registro:', error)
    });
  }
}
