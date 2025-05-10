import {
  Component,
  Input,
  OnInit,
  Optional,
  Self,
  forwardRef
} from '@angular/core';
import {
  ControlValueAccessor,
  NgControl,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
  FormControl
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-password-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule, // 👈 esto es clave
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './password-input.component.html',
  styleUrls: ['./password-input.component.css'],
  
})
export class PasswordInputComponent implements OnInit, ControlValueAccessor {
  @Input() label: string = 'Contraseña';
  @Input() placeholder: string = '';
  @Input() minLength: number = 8;

  @Input() errorMessages: { [key: string]: string } = {
    required: 'Este campo es obligatorio',
    minlength: 'Mínimo 8 caracteres',
  };

  @Input() appearance: 'outline' | 'fill' | 'standard' = 'outline';
  @Input() autocomplete: string = 'current-password';

  hidePassword = true;
  touched = false;

  onChange = (_: any) => {};
  onTouched = () => {};

  constructor(@Optional() @Self() public ngControl: NgControl) {
    if (ngControl) {
      ngControl.valueAccessor = this;
    }
  }

  ngOnInit(): void {
    this.errorMessages['minlength'] = `Mínimo ${this.minLength} caracteres`;
  }

  get control(): FormControl {
    return this.ngControl?.control as FormControl;
  }
  

  get showError(): boolean {
    return this.control?.invalid && (this.control?.touched || this.touched);
  }

  get errorMessage(): string {
    const errors = this.control?.errors || {};

    for (const key in errors) {
      if (this.errorMessages[key]) {
        return this.errorMessages[key];
      }
      if (typeof errors[key] === 'object' && errors[key]?.message) {
        return errors[key].message;
      }
    }

    return 'Error de validación';
  }

  writeValue(value: any): void {}

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {}

  onBlur(): void {
    this.touched = true;
    this.onTouched();
  }

  toggleVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }
}
