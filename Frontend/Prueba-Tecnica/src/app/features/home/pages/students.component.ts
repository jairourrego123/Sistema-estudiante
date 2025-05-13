import { Component, type OnInit } from "@angular/core"
import { CommonModule } from "@angular/common"
import { MatButtonModule } from "@angular/material/button"
import { MatIconModule } from "@angular/material/icon"
import { MatCardModule } from "@angular/material/card"
import { MatDialogModule,  MatDialog } from "@angular/material/dialog"
import { GenericTableComponent, TableColumn } from "../components/generictable/generic-table.component"
import { StudentService } from "../../../core/adapters/student.service"
import { Nombre,  Student } from "../../../models/student"
import { StudentFormComponent } from "../components/forms/student-form.component"


@Component({
  selector: "app-students",
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule, GenericTableComponent],
  template: `
    <div class="page-container">
      <div class="page-header">
        <h1>Compañeros</h1>
        <!-- <button mat-flat-button color="primary" (click)="openStudentForm()">
          <mat-icon>add</mat-icon>
          Nuevo Estudiante
        </button> -->
      </div>
      
      <mat-card>
        <mat-card-content>
          <app-generic-table
            [columns]="columns"
            [data]="classmates"
            [totalItems]="totalStudents"
            [pageSize]="pageSize"
            (onView)="viewStudent($event)"
            (onEdit)="editStudent($event)"
            (onDelete)="deleteStudent($event)"
            (onPageChange)="onPageChange($event)"
            [showActions]="false">

          </app-generic-table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [
    `
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
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
      width: 100%;
    }
  `,
  ],
})
export class StudentsComponent implements OnInit {
  columns: TableColumn[] = [
    { name: "Nombre", property: "nombre", sortable: false, visible: true },
   
  ]

  students: Student[] = []
  pageSize = 10
  currentPage = 0
  classmates: Nombre[] = [];
  totalStudents = this.classmates.length
  
  constructor(
    private studentService: StudentService,
    private dialog: MatDialog,
  ) {}

  ngOnInit() {
    this.loadStudents()
  }

  loadStudents() {
    this.studentService.getClassmates("7a235c0b-9fbb-48d8-8971-bb004e1452e9").subscribe(
      (response) => {
          
          this.classmates = response
          this.totalStudents = response.length 
      },
      (error) => {
       alert(error.error)

      },
    )
  }

  openStudentForm(student?: Student) {
    const dialogRef = this.dialog.open(StudentFormComponent, {
      width: "600px",
      data: { student },
    })

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadStudents()
      }
    })
  }

  viewStudent(student: Student) {
    // Implement view logic
    console.log("View student", student)
  }

  editStudent(student: Student) {
    this.openStudentForm(student)
  }

  deleteStudent(student: Student) {
    if (confirm(`¿Está seguro que desea eliminar al estudiante ${student.nombre} ${student.apellido}?`)) {
      this.studentService.deleteStudent(student.id).subscribe(
        () => {
          this.loadStudents()
        },
        (error) => {
        alert(error.error)
        },
      )
    }
  }

  onPageChange(event: { pageIndex: number; pageSize: number }) {
    this.currentPage = event.pageIndex
    this.pageSize = event.pageSize
    this.loadStudents()
  }
}
