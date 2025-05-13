// src/app/services/professor.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Professor } from '../../models/professor.model';


@Injectable({
  providedIn: 'root',
})
export class ProfessorService {
  private readonly API_URL = `${environment.apiUrlSistema}/profesores`;

  constructor(private http: HttpClient) {}

  /** Obtiene todos los profesores */
  getProfessors(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.API_URL);
  }

  /** Obtiene un profesor por su ID */
  getProfessorById(id: string): Observable<Professor> {
    return this.http.get<Professor>(`${this.API_URL}/${id}`);
  }

  /** Crea un nuevo profesor */
  createProfessor(professor: Omit<Professor, 'id' | 'fechaGrabacion'>)
    : Observable<{ id: string }> {
    // tu API devuelve { id } en el CreatedAtAction
    return this.http.post<{ id: string }>(this.API_URL, professor);
  }

  /** Actualiza un profesor existente */
  updateProfessor(professor: Partial<Professor> & { id: string })
    : Observable<void> {
    return this.http.put<void>(
      `${this.API_URL}/${professor.id}`,
      professor
    );
  }

  /** Elimina un profesor por ID */
  deleteProfessor(id: string): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/${id}`);
  }
}
