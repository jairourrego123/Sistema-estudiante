// src/app/services/registration.service.ts

import { Injectable } from '@angular/core';
import { HttpClient }   from '@angular/common/http';
import { Observable }   from 'rxjs';
import { environment } from '../../../environments/environment';
import { Registration } from '../../models/registration.model';



@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private readonly API_URL = `${environment.apiUrlSistema}/inscripciones`;

  constructor(private http: HttpClient) {}

  /** Trae todas mis inscripciones */
  getRegistrations(userId: string): Observable<Registration[]> {
    return this.http.get<Registration[]>(`${this.API_URL}?userId=${userId}`);
  }

  /** Inscribe materias para un estudiante */
  inscribirMaterias(
    userId: string,
    materiaIds: string[]
  ): Observable<void> {
    return this.http.post<void>(
      this.API_URL,
      { userId, materiaIds }
    );
  }

  /** Desinscribe una materia de un estudiante */
  desinscribirMateria(
    userId: string,
    materiaId: string
  ): Observable<void> {
    return this.http.request<void>(
      'delete',
      this.API_URL,
      { body: { userId, materiaId } }
    );
  }
}
