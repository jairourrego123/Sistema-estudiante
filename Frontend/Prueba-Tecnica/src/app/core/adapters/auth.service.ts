import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import {jwtDecode} from 'jwt-decode';
import { Tokens } from '../../models/tokens';
import { environment } from '../../../environments/environment';
import { Usuario } from '../../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<Tokens> {
    return this.http.post<Tokens>(`${this.apiUrl}/login`, { email:username, password }).pipe(
      tap(tokens => this.storeTokens(tokens))
    );
  }

  register(user: Usuario): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/registrar-usuario`, user);
  }

  private storeTokens(tokens: Tokens): void {
    localStorage.setItem('accessToken', tokens.accessToken);
    localStorage.setItem('refreshToken', tokens.refreshToken);
  }

  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  isAuthenticated(): boolean {
    const token = this.getAccessToken();
    if (!token) return false;

    try {
      const decoded: any = jwtDecode(token);
      return decoded && decoded.exp > (Date.now() / 1000);
    } catch (error) {
      console.error('Error al decodificar el token:', error);
      return false;
    }
  }
}
