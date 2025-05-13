// swal.service.ts
import { Injectable } from '@angular/core';
import Swal, { SweetAlertOptions, SweetAlertResult } from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class SwalService {

    showAlert(options: SweetAlertOptions): Promise<SweetAlertResult<any>> {
    return Swal.fire(options);
  }

  //alerta de confirmación
  showConfirm(options: SweetAlertOptions): Promise<SweetAlertResult<any>> {
    return Swal.fire({
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Sí',
      cancelButtonText: 'No',
      ...options,
    });
  }

  //alerta de éxito
  showSuccess(): Promise<SweetAlertResult<any>> {
    return Swal.fire({
      icon: 'success',
      title:'¡Éxito!',
      text:'La operación se completó correctamente',
    });
  }

  // alerta de error
  showError(text?: string): Promise<SweetAlertResult<any>> {
    return Swal.fire({
      icon: 'error',
      title: '¡Error!',
      text ,
    });
  }

  // alerta de información
  showInfo(title: string, text?: string): Promise<SweetAlertResult<any>> {
    return Swal.fire({
      icon: 'info',
      title,
      text,
    });
  }

  //alerta de advertencia
  showWarning(title: string, text?: string): Promise<SweetAlertResult<any>> {
    return Swal.fire({
      icon: 'warning',
      title,
      text,
    });
  }
}
