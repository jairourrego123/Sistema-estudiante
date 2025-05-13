import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
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
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ProfessorService } from '../../../../core/adapters/professor.service';
import { Professor } from '../../../../models/professor.model';
import { SwalService } from '../../../../core/adapters/alert.service';



@Component({
  selector: 'app-professor-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ],
  template: `
    <h2 mat-dialog-title>
      {{ isEditMode ? 'Editar Profesor' : 'Nuevo Profesor' }}
    </h2>
    <form [formGroup]="professorForm" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Nombre</mat-label>
          <input
            matInput
            formControlName="nombre"
            placeholder="Nombre del profesor"
          />
          <mat-error *ngIf="professorForm.get('nombre')?.hasError('required')">
            El nombre es requerido
          </mat-error>
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button mat-dialog-close type="button">Cancelar</button>
        <button
          mat-flat-button
          color="primary"
          type="submit"
          [disabled]="professorForm.invalid"
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
      }
      mat-dialog-content {
        min-width: 400px;
        padding-top: 16px;
      }
    `
  ]
})
export class ProfessorFormComponent implements OnInit {
  professorForm!: FormGroup;
  isEditMode = false;

  constructor(
    private swalService: SwalService ,
    private fb: FormBuilder,
    private professorService: ProfessorService,
    private dialogRef: MatDialogRef<ProfessorFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { professor?: Professor }
  ) {}

  ngOnInit(): void {
    this.isEditMode = !!this.data.professor;

    this.professorForm = this.fb.group({
      nombre: [this.data.professor?.nombre || '', Validators.required]
    });
  }

  onSubmit() {
    if (this.professorForm.invalid) {
      return;
    }

    const formValue = this.professorForm.value;

    if (this.isEditMode && this.data.professor) {
      const updated: Partial<Professor> & { id: string } = {
        id: this.data.professor.id,
        nombre: formValue.nombre
      };
      this.professorService.updateProfessor(updated).subscribe({
        next: () => {
          this.swalService.showSuccess() 
          return this.dialogRef.close(true);
        },
        error: err => this.swalService.showError(err.error.message)
      });
    } else {
      this.professorService
        .createProfessor({ nombre: formValue.nombre })
        .subscribe({
          next: ({ id }) => {
            this.swalService.showSuccess() 
            return this.dialogRef.close(id);
          },
          error: err => this.swalService.showError(err.error.message)
        });
    }
  }
}
