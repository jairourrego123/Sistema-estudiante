// src/app/components/professors/professors.component.ts

import { Component, OnInit } from '@angular/core';
import { CommonModule }       from '@angular/common';
import { MatButtonModule }    from '@angular/material/button';
import { MatIconModule }      from '@angular/material/icon';
import { MatCardModule }      from '@angular/material/card';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';

import {
  GenericTableComponent,
  TableColumn
} from '../components/generictable/generic-table.component';


import { ProfessorFormComponent } from '../components/forms/professor-form.component';
import { Professor } from '../../../models/professor.model';
import { ProfessorService } from '../../../core/adapters/professor.service';
import { SwalService } from '../../../core/adapters/alert.service';

@Component({
  selector: 'app-professors',
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
        <h1>Profesores</h1>
        <button mat-flat-button color="primary" class="button-add" (click)="openProfessorForm()">
          <mat-icon>add</mat-icon>
          Nuevo Profesor
        </button>
      </div>
      
      <mat-card>
        <mat-card-content>
          <app-generic-table
            [columns]="columns"
            [data]="professors"
            [totalItems]="totalStudents"
            (onView)="viewProfessor($event)"
            (onEdit)="editProfessor($event)"
            (onDelete)="deleteProfessor($event)">
            
          </app-generic-table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .button-add{
      background-color:#1abc9c !important
    }
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
export class ProfessorsComponent implements OnInit {
  columns: TableColumn[] = [
    { name: 'ID',         property: 'id',             sortable: false, visible: false },
    { name: 'Nombre',     property: 'nombre',         sortable: false, visible: true },
    { name: 'Fecha Registro', property: 'fechaGrabacion', sortable: false, visible: true }
  ];

  professors: Professor[] = [];
  totalStudents = this.professors.length
  constructor(
    private swalService: SwalService ,
    private professorService: ProfessorService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadProfessors();
  }

  loadProfessors() {
    this.professorService.getProfessors().subscribe({
      next: (data: Professor[]) => {
        this.professors = data;
        this.totalStudents = data.length
      },
      error: err => this.swalService.showError(err.error.message)

    });
  }

  openProfessorForm(professor?: Professor) {
    const dialogRef = this.dialog.open(ProfessorFormComponent, {
      width: '600px',
      data: { professor }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadProfessors();
      }
    });
  }

  viewProfessor(professor: Professor) {
    console.log('View professor', professor);
  }

  editProfessor(professor: Professor) {
    this.openProfessorForm(professor);
  }

  deleteProfessor(professor: Professor) {
    if (confirm(`¿Eliminar profesor "${professor.nombre}"?`)) {
      this.professorService.deleteProfessor(professor.id).subscribe({
        next: () => {
            this.swalService.showSuccess() 
            return this.loadProfessors();
          },
        error: err => this.swalService.showError(err.error.message)
    });
    }
  }
}
