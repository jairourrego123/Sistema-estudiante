// src/app/services/subject.service.ts

import { Injectable } from '@angular/core';
import { HttpClient }   from '@angular/common/http';
import { Observable }   from 'rxjs';
import { environment } from '../../../environments/environment';
import { Subject } from '../../models/subject.model';

@Injectable({
  providedIn: 'root',
})
export class SubjectService {
  private readonly API_URL = `${environment.apiUrlSistema}/materias`;

  constructor(private http: HttpClient) {}

  /** Obtiene todas las materias (subjects) */
  getSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(this.API_URL);
  }

  /** Obtiene una materia por su ID */
  getSubjectById(id: string): Observable<Subject> {
    return this.http.get<Subject>(`${this.API_URL}/${id}`);
  }

  /** Crea una nueva materia */
  createSubject(dto: Omit<Subject, 'id' | 'fechaGrabacion'>)
    : Observable<void> {
    return this.http.post<void>(this.API_URL, dto);
  }

  /** Edita una materia existente (puede cambiar nombre y/o profesorId) */
  updateSubject(subject: Partial<Subject> & { id: string })
    : Observable<void> {
    return this.http.put<void>(
      `${this.API_URL}/${subject.id}`,
      subject
    );
  }

  /** Elimina una materia por ID */
  deleteSubject(id: string): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/${id}`);
  }
}
