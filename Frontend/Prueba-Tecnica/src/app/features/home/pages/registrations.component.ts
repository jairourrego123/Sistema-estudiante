// src/app/components/registrations/registrations.component.ts

import { Component, OnInit } from '@angular/core';
import { CommonModule }      from '@angular/common';
import { MatCardModule }     from '@angular/material/card';
import { MatButtonModule }   from '@angular/material/button';
import { MatIconModule }     from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';

import { GenericTableComponent, TableColumn } from '../components/generictable/generic-table.component';
import { Registration }        from '../../../models/registration.model';
import { RegistrationService } from '../../../core/adapters/registration.service';
import { RegistrationFormComponent } from '../components/forms/registration-form.component';
import { SwalService } from '../../../core/adapters/alert.service';
import { AuthService } from '../../../core/adapters/auth.service';

@Component({
  selector: 'app-registrations',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    GenericTableComponent,
  ],
  template: `
    <div class="page-container">
      <div class="page-header">
        <h1>Inscripciones</h1>
        <button mat-flat-button color="primary" class="button-add" (click)="openRegistrationForm()">
          <mat-icon>add</mat-icon>
          Nueva Inscripción
        </button>
      </div>
      
      <mat-card>
        <mat-card-content>
          <app-generic-table
            [columns]="columns"
            [data]="registrations"
            [totalItems]="registrations.length"
            (onDelete)="desinscribir($event)">
          </app-generic-table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .button-add{
      background-color:#1abc9c !important
    }
    .page-container { padding: 24px; width: 100%; }
    .page-header {
      display: flex; justify-content: space-between; align-items: center;
      margin-bottom: 24px;
    }
    h1 { margin: 0; font-size: 24px; font-weight: 500; }
    mat-card {
      border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); width: 100%;
    }
  `]
})
export class RegistrationsComponent implements OnInit {
  columns: TableColumn[] = [
    { name: 'ID',         property: 'id',             sortable: true, visible: false },
    { name: 'Estudiante', property: 'estudianteId',   sortable: true, visible: false },
    { name: 'Materia',    property:  'materiaNombre' ,     sortable: true, visible: true },
    { name: 'Creditos',    property:  'creditos' ,     sortable: true, visible: true },
    { name: 'Profesor',      property: 'profesorNombre', sortable: true, visible: true }
  ];

  registrations: Registration[] = [];

  userId = ""
  constructor(
    private swalService: SwalService ,
    private registrationService: RegistrationService,
    private dialog: MatDialog,
    private authService: AuthService 
  ) {
    this.userId = this.authService.getUserId() ?? "";

  }
  

  ngOnInit(): void {
    this.loadRegistrations();
    console.log(this.registrations);
    
  }

  private loadRegistrations(): void {
    this.registrationService.getRegistrations(this.userId).subscribe({
      next: (data: Registration[]) => this.registrations = data,
      error: err => this.swalService.showError(err.error.message)

    });
  }

  desinscribir(reg: Registration): void {
    if (!confirm(`¿Desinscribir materia ${reg.materiaNombre}?`)) {
      return;
    }
    this.registrationService.desinscribirMateria(this.userId, reg.materiaId).subscribe({
      next: () => {
            this.swalService.showSuccess() 
            return this.loadRegistrations();
        },
      error: err => this.swalService.showError(err.error.message)
    });
  }

  openRegistrationForm(): void {
    const dialogRef = this.dialog.open(RegistrationFormComponent, {
      width: '500px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadRegistrations();
      }
    });
  }
}
