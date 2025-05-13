import { Component, Inject, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from "@angular/forms";
import {
  MatDialogModule,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from "@angular/material/dialog";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";

import { Student } from "../../../../models/student";
import { StudentService } from "../../../../core/adapters/student.service";
import { SwalService } from "../../../../core/adapters/alert.service";

@Component({
  selector: "app-student-form",
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  template: `
    <h2 mat-dialog-title>
      {{ isEditMode ? 'Editar Estudiante' : 'Nuevo Estudiante' }}
    </h2>
    <form [formGroup]="studentForm" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Nombre</mat-label>
          <input matInput formControlName="nombre" />
          <mat-error *ngIf="nombre.invalid">
            El nombre es requerido
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Apellido</mat-label>
          <input matInput formControlName="apellido" />
          <mat-error *ngIf="apellido.invalid">
            El apellido es requerido
          </mat-error>
        </mat-form-field>
      </mat-dialog-content>

      <mat-dialog-actions align="end">
        <button mat-button mat-dialog-close type="button">Cancelar</button>
        <button
          mat-flat-button
          color="primary"
          type="submit"
          [disabled]="studentForm.invalid"
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
    `,
  ],
})
export class StudentFormComponent implements OnInit {
  studentForm!: FormGroup;
  isEditMode = false;

  constructor(
    private swalService: SwalService ,
    private fb: FormBuilder,
    private studentService: StudentService,
    private dialogRef: MatDialogRef<StudentFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { student?: Student }
  ) {}

  ngOnInit() {
    this.isEditMode = !!this.data.student;

    this.studentForm = this.fb.group({
      nombre: [this.data.student?.nombre || "", Validators.required],
      apellido: [this.data.student?.apellido || "", Validators.required],
    });
  }

  get nombre() {
    return this.studentForm.get("nombre")!;
  }
  get apellido() {
    return this.studentForm.get("apellido")!;
  }

  onSubmit() {
    if (this.studentForm.invalid) return;

    const { nombre, apellido } = this.studentForm.value;

    if (this.isEditMode && this.data.student) {
      const updated: Student = {
        id: this.data.student.id,
        userId: this.data.student.userId,
        nombre,
        apellido,
      };
      this.studentService.updateStudent(updated).subscribe({
        next: () => {
          this.swalService.showSuccess() 
          return this.dialogRef.close(true);
        },
        error: err => this.swalService.showError(err.error.message)
      });
    } else {
      this.studentService
        .createStudent({ nombre, apellido,userId:""})
        .subscribe({
          next: (newId) => {
            this.swalService.showSuccess() 
            return this.dialogRef.close(newId);
          },
          error: err => this.swalService.showError(err.error.message)
        });
    }
  }
}
