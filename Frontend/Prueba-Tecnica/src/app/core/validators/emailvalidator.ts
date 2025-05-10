import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class EmailValidator {
  static validEmail(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      if (!value) return null;

      const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|es|net|org|co|edu)$/;

      return emailRegex.test(value)
        ? null
        : {
            validEmail: {
              message: 'Formato inválido de correo',
            },
          };
    };
  }
}
