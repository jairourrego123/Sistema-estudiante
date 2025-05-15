import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import {jwtDecode} from 'jwt-decode';
import { AccessToken, AuthTokens } from '../../models/tokens';
import { environment } from '../../../environments/environment';
import { RegistroResponse, Usuario } from '../../models/usuario';

interface DecodedToken {
  nameid: string;       
  email: string;
  given_name: string;   
  family_name: string;  
  nbf: number;
  exp: number;
  iat: number;
  iss: string;
  aud: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrlAutenticacion}/auth`;
  private apiUrlServicio = `${environment.apiUrlSistema}/estudiantes`;

  private userSubject = new BehaviorSubject<DecodedToken | null>(null);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromToken();
  }

  login(username: string, password: string): Observable<AuthTokens> {
    return this.http.post<AuthTokens>(`${this.apiUrl}/login`, { email: username, password })
      .pipe(
        tap(tokens => {
          this.storeTokens(tokens);
          this.updateUserFromToken(tokens.accessToken);
        })
      );
  }

  refreshToken(token: string): Observable<AccessToken> {
    return this.http.post<AccessToken>(`${this.apiUrl}/token/refresh`, { refreshToken: token });
  }

  register(user: Usuario): Observable<void> {
    return this.http.post<RegistroResponse>(`${this.apiUrl}/registrar-usuario`, user).pipe(
      this.CrearEstudiante(user)
    );
  }

 
  generarEnlaceRestablecimiento(email: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/password/enlace-restablecimiento`, { email });
  }

  restablecerContraseña(email: string, token: string, nuevaContrasena: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/password/restablecer`, { email, token, nuevaContrasena });
  }
  
  private CrearEstudiante(user: Usuario) {
    return switchMap((response: RegistroResponse): Observable<void> => {
      const userId = response.userId;
      const nombre = user.nombre;
      const apellido = user.apellido;
      return this.http.post<void>(this.apiUrlServicio, { userId, nombre, apellido });
    });
  }

  private storeTokens(tokens: AuthTokens): void {
    localStorage.setItem('accessToken', tokens.accessToken);
    localStorage.setItem('refreshToken', tokens.refreshToken);
  }

  public getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  private loadUserFromToken(): void {
    const token = this.getAccessToken();
    if (token) this.updateUserFromToken(token);
  }

  private updateUserFromToken(token: string): void {
    try {
      const decoded = jwtDecode<DecodedToken>(token);
      this.userSubject.next(decoded);
    } catch (err) {
      console.error('Error al decodificar el token:', err);
      this.userSubject.next(null);
    }
  }

  getUserId(): string | null {
    return this.userSubject.value?.nameid ?? null;
  }

  getUserFullName(): string | null {
    const u = this.userSubject.value;
    return u ? `${u.given_name.trim()} ${u.family_name.trim()}`.trim() : null;
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this.userSubject.next(null);
  }

  isAuthenticated(): boolean {
    const token = this.getAccessToken();
    if (!token) return false;

    try {
      const decoded = jwtDecode<DecodedToken>(token);
      return decoded.exp > (Date.now() / 1000);
    } catch (error) {
      console.error('Error al decodificar el token:', error);
      return false;
    }
  }
}
