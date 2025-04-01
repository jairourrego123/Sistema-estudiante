import { HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../adapters/auth.service';
import { inject } from '@angular/core';

export const AuthInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
  const authService = inject(AuthService);
  const token = authService.getAccessToken();

  const authReq = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(authReq).pipe(
    catchError(err => {
      if (err.status === 401 && token) {
        return authService.refreshToken(token).pipe(

          // SwitchMap es un operador de transformacion de observaciones
          // Cancela la suscripcion anterior y ejecuta una nueva llamada basa en el resultado anterior 
          // Devuelve el observabe mas reciente  
          switchMap(newToken => { 
            const newReq = req.clone({
              setHeaders: {
                Authorization: `Bearer ${newToken.accessToken}`
              }
            });
            return next(newReq);
          }),
          catchError(refreshErr => {
            authService.logout(); 
            return throwError(() => refreshErr);
          })
        );
      }

      return throwError(() => err);
    })
  );
};
