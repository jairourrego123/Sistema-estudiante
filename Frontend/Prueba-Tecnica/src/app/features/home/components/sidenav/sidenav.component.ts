import { Component } from "@angular/core"
import { CommonModule } from "@angular/common"
import { MatListModule } from "@angular/material/list"
import { MatIconModule } from "@angular/material/icon"
import { MatExpansionModule } from "@angular/material/expansion"
import { RouterModule } from "@angular/router"

@Component({
  selector: "app-sidenav",
  standalone: true,
  imports: [CommonModule, MatListModule, MatIconModule, MatExpansionModule, RouterModule],
  template: `
    <div class="sidenav-header">
      <div class="logo-container">
        <mat-icon>school</mat-icon>
        <span class="logo-text">Sistema Acdemico</span>
      </div>
    </div>
    
    <div class="sidenav-content">

      
      <div class="nav-section">
        <div class="section-title">Academia</div>
        <mat-nav-list>
          <a mat-list-item routerLink="/academic/students" routerLinkActive="active-link">
            <mat-icon>people</mat-icon>
            <span>Compañeros</span>
          </a>
         
          <a mat-list-item routerLink="/academic/subjects" routerLinkActive="active-link">
            <mat-icon>book</mat-icon>
            <span>Materias</span>
          </a>
          <a mat-list-item routerLink="/academic/registrations" routerLinkActive="active-link">
            <mat-icon>assignment</mat-icon>
            <span>Inscripciones</span>
          </a>
        </mat-nav-list>
      </div>
      <div class="nav-section">
        <div class="section-title">Administrador</div>
        <mat-nav-list>
          <!-- <a mat-list-item routerLink="/academic/students" routerLinkActive="active-link">
            <mat-icon>people</mat-icon>
            <span>Estudiantes</span>
          </a> -->
          <a mat-list-item routerLink="/academic/professors" routerLinkActive="active-link">
            <mat-icon>school</mat-icon>
            <span>Profesores</span>
          </a>
          <!-- <a mat-list-item routerLink="/academic/subjects" routerLinkActive="active-link">
            <mat-icon>book</mat-icon>
            <span>Materias</span>
          </a>
          <a mat-list-item routerLink="/academic/registrations" routerLinkActive="active-link">
            <mat-icon>assignment</mat-icon>
            <span>Inscripciones</span>
          </a> -->
        </mat-nav-list>
      </div>
    </div>
  `,
  styles: [
    `
    .sidenav-header {
      height: 64px;
      display: flex;
      align-items: center;
      padding: 0 16px;
      border-bottom: 1px solid #e9ecef;
    }
    
    .logo-container {
      display: flex;
      align-items: center;
    }
    
    .logo-text {
      margin-left: 8px;
      font-size: 18px;
      font-weight: 500;
    }
    
    .sidenav-content {
      padding: 16px 0;
    }
    
    .nav-section {
      margin-bottom: 24px;
    }
    
    .section-title {
      padding: 0 16px;
      margin-bottom: 8px;
      font-size: 12px;
      font-weight: 500;
      color: #6c757d;
    }
    
    mat-nav-list {
      padding-top: 0;
    }
    
    mat-nav-list a {
      height: 40px;
      font-size: 14px;
    }
    
    mat-nav-list a mat-icon {
      margin-right: 16px;
      color: #6c757d;
    }
    
    .active-link {
      background-color: rgba(63, 81, 181, 0.1);
      color: #3f51b5;
    }
    
    .active-link mat-icon {
      color: #3f51b5 !important;
    }
  `,
  ],
})
export class SidenavComponent {}
