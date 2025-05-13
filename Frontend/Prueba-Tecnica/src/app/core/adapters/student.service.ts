import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CrearEstudianteCommand, EditarEstudianteCommand, Nombre, Student } from '../../models/student';
import { environment } from '../../../environments/environment';



@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private readonly API_URL = `${environment.apiUrlSistema}/estudiantes`;

  constructor(private http: HttpClient) {}

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.API_URL);
  }

  getStudentById(id: string): Observable<Student> {
    return this.http.get<Student>(`${this.API_URL}/${id}`);
  }


  createStudent(cmd: Omit<CrearEstudianteCommand, 'id'>): Observable<string> {
    return this.http
      .post<{ id: string }>(this.API_URL, cmd)
      .pipe(map(response => response.id));
  }


  updateStudent(cmd: EditarEstudianteCommand): Observable<void> {
    return this.http.put<void>(`${this.API_URL}/${cmd.id}`, cmd);
  }

  deleteStudent(id: string): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/${id}`);
  }

  /**
   * GET /api/estudiantes/companeros?materiaId={}&userId={}
   * Response: List<string> (IDs o nombres de compañeros)
   */
  getClassmates(userId: string): Observable<Nombre[]> {
    const params = new HttpParams()
      .set('userId', userId);

    return this.http.get<Nombre[]>(`${this.API_URL}/companeros`, { params });
  }
}
