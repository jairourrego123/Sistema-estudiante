// src/app/components/subjects/subject-form.component.ts

import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule }                from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import {
  MatDialogModule,
  MAT_DIALOG_DATA,
  MatDialogRef
} from '@angular/material/dialog';
import { MatButtonModule }    from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule }     from '@angular/material/input';
import { MatSelectModule }    from '@angular/material/select';
import { Professor } from '../../../../models/professor.model';
import { SubjectService } from '../../../../core/adapters/subject.service';
import { ProfessorService } from '../../../../core/adapters/professor.service';
import { Subject } from '../../../../models/subject.model';
import { SwalService } from '../../../../core/adapters/alert.service';


@Component({
  selector: 'app-subject-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ],
  template: `
    <h2 mat-dialog-title>
      {{ isEditMode ? 'Editar Materia' : 'Nueva Materia' }}
    </h2>
    <form [formGroup]="subjectForm" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Nombre</mat-label>
          <input matInput formControlName="nombre" placeholder="Nombre de la materia" />
          <mat-error *ngIf="subjectForm.get('nombre')?.hasError('required')">
            El nombre es requerido
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Créditos</mat-label>
          <input
            matInput
            type="number"
            formControlName="creditos"
            placeholder="Créditos"
          />
          <mat-error *ngIf="subjectForm.get('creditos')?.hasError('required')">
            Los créditos son requeridos
          </mat-error>
          <mat-error *ngIf="subjectForm.get('creditos')?.hasError('min')">
            Debe ser al menos 1 crédito
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Profesor</mat-label>
          <mat-select formControlName="profesorId">
            <mat-option
              *ngFor="let prof of professors"
              [value]="prof.id"
            >
              {{ prof.nombre }}
            </mat-option>
          </mat-select>
          <mat-error *ngIf="subjectForm.get('profesorId')?.hasError('required')">
            El profesor es requerido
          </mat-error>
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button mat-dialog-close type="button">Cancelar</button>
        <button
          mat-flat-button
          color="primary"
          type="submit"
          [disabled]="subjectForm.invalid"
        >
          {{ isEditMode ? 'Actualizar' : 'Guardar' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [
    `
      .full-width {
        width: 100%;
        margin-bottom: 16px;
      }
      mat-dialog-content {
        min-width: 400px;
        padding-top: 16px;
      }
    `
  ]
})
export class SubjectFormComponent implements OnInit {
  subjectForm!: FormGroup;
  isEditMode = false;
  professors: Professor[] = [];

  constructor(
    private swalService: SwalService ,
    private fb: FormBuilder,
    private subjectService: SubjectService,
    private professorService: ProfessorService,
    private dialogRef: MatDialogRef<SubjectFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { subject?: Subject }
  ) {}

  ngOnInit(): void {
    this.isEditMode = !!this.data.subject;

    this.subjectForm = this.fb.group({
      nombre: [
        this.data.subject?.nombre || '',
        Validators.required
      ],
      creditos: [
        this.data.subject?.creditos ?? 3,
        [Validators.required, Validators.min(1)]
      ],
      profesorId: [
        this.data.subject?.profesorId || '',
        Validators.required
      ]
    });

    this.loadProfessors();
  }

  private loadProfessors(): void {
    this.professorService.getProfessors().subscribe({
      next: (data: Professor[]) => (this.professors = data),
      error: err => this.swalService.showError(err.error.message)
    });
  }

  onSubmit(): void {
    if (this.subjectForm.invalid) return;

    const formValue = this.subjectForm.value;

    if (this.isEditMode && this.data.subject) {
      this.subjectService
        .updateSubject({
          id: this.data.subject.id,
          nombre: formValue.nombre,
          creditos: formValue.creditos,
          profesorId: formValue.profesorId
        })
        .subscribe({
          next: () => {
                this.swalService.showSuccess() 
                return this.dialogRef.close(true);
            },
          error: err => this.swalService.showError(err.error.message)
        });
    } else {
      this.subjectService
        .createSubject({
          nombre: formValue.nombre,
          creditos: formValue.creditos,
          profesorId: formValue.profesorId,
          profesorNombre:formValue.profesorNombre,
        })
        .subscribe({
        next: () => {
                this.swalService.showSuccess() 
                return this.dialogRef.close(true);
            },
        error: err => this.swalService.showError(err.error.message)

        });
    }
  }
}
