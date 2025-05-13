// src/app/components/subjects/subjects.component.ts

import { Component, OnInit } from '@angular/core';
import { CommonModule }      from '@angular/common';
import { MatButtonModule }   from '@angular/material/button';
import { MatIconModule }     from '@angular/material/icon';
import { MatCardModule }     from '@angular/material/card';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';

import {
  GenericTableComponent,
  TableColumn
} from '../components/generictable/generic-table.component';


import { SubjectFormComponent } from '../components/forms/subject-form.component';
import { Subject } from '../../../models/subject.model';
import { SubjectService } from '../../../core/adapters/subject.service';
import { SwalService } from '../../../core/adapters/alert.service';

@Component({
  selector: 'app-subjects',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    GenericTableComponent
  ],
  template: `
    <div class="page-container">
      <div class="page-header">
        <h1>Materias</h1>
        <button mat-flat-button color="primary" (click)="openSubjectForm()">
          <mat-icon>add</mat-icon>
          Nueva Materia
        </button>
      </div>
      
      <mat-card>
        <mat-card-content>
          <app-generic-table
            [columns]="columns"
            [data]="subjects"
            [totalItems]="subjects.length"
            (onView)="viewSubject($event)"
            (onEdit)="editSubject($event)"
            (onDelete)="deleteSubject($event)">
          </app-generic-table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .page-container {
      padding: 24px;
      width: 100%;
    }
    .page-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }
    h1 {
      margin: 0;
      font-size: 24px;
      font-weight: 500;
    }
    mat-card {
      border-radius: 8px;
      box-shadow: 0 2px 8px rgba(0,0,0,0.1);
      width: 100%;
    }
  `]
})
export class SubjectsComponent implements OnInit {
  columns: TableColumn[] = [
    { name: 'Nombre',    property: 'nombre',     sortable: false, visible: true },
    { name: 'Créditos',  property: 'creditos',   sortable: false, visible: true },
    { name: 'Profesor',property: 'profesorNombre', sortable: false, visible: true }
  ];

  subjects: Subject[] = [];

  constructor(
    private swalService: SwalService ,
    private subjectService: SubjectService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadSubjects();
  }

  loadSubjects(): void {
    this.subjectService.getSubjects().subscribe({
      next: (data: Subject[]) => this.subjects = data,
      error: (err: any) =>  alert(err.error)
    });
  }

  openSubjectForm(subject?: Subject): void {
    const ref = this.dialog.open(SubjectFormComponent, {
      width: '600px',
      data: { subject }
    });
    ref.afterClosed().subscribe(result => {
      if (result) {
        this.loadSubjects();
      }
    });
  }

  viewSubject(subject: Subject): void {
    console.log('View subject', subject);
    // could open a read-only detail dialog
  }

  editSubject(subject: Subject): void {
    this.openSubjectForm(subject);
  }

  deleteSubject(subject: Subject): void {
    // this.swalService.showConfirm()
    if (confirm(`¿Eliminar materia "${subject.nombre}"?`)) {
      this.subjectService.deleteSubject(subject.id).subscribe({
        next: () => {
            this.swalService.showSuccess() 
            return this.loadSubjects();
          },
        error: err => this.swalService.showError(err.error.message)
    });
    }
  }
}
