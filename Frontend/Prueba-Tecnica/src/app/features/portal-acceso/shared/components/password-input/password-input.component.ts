import { Component, Input, OnInit, forwardRef, OnDestroy } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR, ReactiveFormsModule, NgControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-password-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './password-input.component.html',
  styleUrls: ['./password-input.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PasswordInputComponent),
      multi: true
    }
  ]
})
export class PasswordInputComponent implements OnInit, OnDestroy, ControlValueAccessor {
  @Input() label: string = 'Contraseña';
  @Input() placeholder: string = '';
  @Input() minLength: number = 8;
  @Input() errorMessages: { [key: string]: string } = {
    required: 'Este campo es obligatorio',
    minlength: `Mínimo ${this.minLength} caracteres`
  };
  @Input() appearance: 'outline' | 'fill' | 'standard' = 'outline';
  @Input() autocomplete: string = 'current-password';

  hidePassword: boolean = true;
  innerControl = new FormControl('');
  disabled: boolean = false;
  touched: boolean = false;
  parentErrors: any = null;
  
  private valueChanges?: Subscription;

  onChange: any = () => {};
  onTouched: any = () => {};

  constructor() {}

  ngOnInit(): void {
    // Actualiza el mensaje de minlength cuando cambia el valor
    this.errorMessages['minlength'] = `Mínimo ${this.minLength} caracteres`;
    
    this.valueChanges = this.innerControl.valueChanges.subscribe(value => {
      this.onChange(value);
    });
  }
  
  ngOnDestroy(): void {
    if (this.valueChanges) {
      this.valueChanges.unsubscribe();
    }
  }

  toggleVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }

  // ControlValueAccessor interface
  writeValue(value: any): void {
    if (value !== undefined) {
      this.innerControl.setValue(value, { emitEvent: false });
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
    if (isDisabled) {
      this.innerControl.disable({ emitEvent: false });
    } else {
      this.innerControl.enable({ emitEvent: false });
    }
  }

  onBlur(): void {
    if (!this.touched) {
      this.touched = true;
      this.onTouched();
    }
  }

  get showError(): boolean {
    return (this.innerControl.invalid || this.parentErrors) && this.touched;
  }

  get errorMessage(): string {
    if (this.innerControl.errors) {
      for (const errorKey in this.innerControl.errors) {
        if (this.errorMessages[errorKey]) {
          return this.errorMessages[errorKey];
        }
      }
    }
    
    // Luego revisar errores del padre (como required)
    if (this.parentErrors) {
      for (const errorKey in this.parentErrors) {
        if (this.errorMessages[errorKey]) {
          return this.errorMessages[errorKey];
        }
      }
    }
    
    return 'Error de validación';
  }
  
  // Método para actualizar los errores del control padre
  updateErrors(errors: any): void {
    this.parentErrors = errors;
  }
}