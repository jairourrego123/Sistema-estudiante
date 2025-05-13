import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule }               from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl
} from '@angular/forms';
import {
  MatDialogModule,
  MAT_DIALOG_DATA,
  MatDialogRef
} from '@angular/material/dialog';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';   
import { MatButtonModule }     from '@angular/material/button';
import { MatFormFieldModule }  from '@angular/material/form-field';
import { MatSelectModule }     from '@angular/material/select';
import { SubjectService } from '../../../../core/adapters/subject.service';
import { RegistrationService } from '../../../../core/adapters/registration.service';
import { Subject } from '../../../../models/subject.model';
import { SwalService } from '../../../../core/adapters/alert.service';


@Component({
  selector: 'app-registration-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule
  ],
  template: `
    <h2 mat-dialog-title>Inscribir Materias</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field appearance="outline" class="full-width">
          <mat-label>Selecciona hasta 3 materias</mat-label>
          <mat-select formControlName="materiaIds" multiple>
            <mat-option *ngFor="let s of subjects" [value]="s.id">
              {{ s.nombre }}
            </mat-option>
          </mat-select>
          <mat-error *ngIf="form.controls['materiaIds'].hasError('required')">
            Debes seleccionar al menos una materia
          </mat-error>
          <mat-error *ngIf="form.controls['materiaIds'].hasError('maxSelection')">
            Solo puedes seleccionar hasta 3 materias
          </mat-error>
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button mat-dialog-close type="button">Cancelar</button>
        <button mat-flat-button color="primary" type="submit" [disabled]="form.invalid">
          Inscribir
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .full-width { width: 100%; margin-bottom: 16px; }
    mat-dialog-content { min-width: 400px; }
  `]
})
export class RegistrationFormComponent implements OnInit {
  form!: FormGroup;
  subjects: Subject[] = [];

  constructor(
    private swalService: SwalService ,
    private fb: FormBuilder,
    private subjectService: SubjectService,
    private registrationService: RegistrationService,
    private dialogRef: MatDialogRef<RegistrationFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { userId: string }
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      materiaIds: [[], [Validators.required, this.maxSelectionValidator(3)]]
    });

    this.subjectService.getSubjects().subscribe({
      next: (subs: Subject[]) => this.subjects = subs,
      error: err => this.swalService.showError(err.error.message)
    });
  }

  private maxSelectionValidator(max: number) {
    return (ctrl: AbstractControl) =>
      (ctrl.value as any[]).length > max
        ? { maxSelection: true }
        : null;
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const userId = this.data.userId;
    const materiaIds: string[] = this.form.value.materiaIds;

    this.registrationService.inscribirMaterias(userId, materiaIds).subscribe({
      next: () => {
            this.swalService.showSuccess() 
            return this.dialogRef.close(true);
        },
      error: err => this.swalService.showError(err.error.message)
    });
  }
}

