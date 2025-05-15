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
        <div class="logo-wrapper">
          <img src="https://www.prooving.com/img/curiers/inter.png" alt="Logo" class="logo-image" />
        </div>
        <div class="header-divider"></div>
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
          
          <a mat-list-item routerLink="/academic/registrations" routerLinkActive="active-link">
            <mat-icon>assignment</mat-icon>
            <span>Inscripciones</span>
          </a>
        </mat-nav-list>
      </div>
      <div class="nav-section">
        <div class="section-title">Administrador</div>
        <mat-nav-list>
          <a mat-list-item routerLink="/academic/professors" routerLinkActive="active-link">
            <mat-icon>school</mat-icon>
            <span>Profesores</span>
          </a>
          <a mat-list-item routerLink="/academic/subjects" routerLinkActive="active-link">
            <mat-icon>book</mat-icon>
            <span>Materias</span>
          </a>
        </mat-nav-list>
      </div>
    </div>
  `,
  styles: [
    `
    :host {
      display: flex;
      flex-direction: column;
      height: 100%;
      background-color: #ffffff;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
    }
    
    .sidenav-header {
      height: 70px;
      display: flex;
      flex-direction: column;
      justify-content: center;
      // background: linear-gradient(to right, #2c3e50, #34495e);
      position: relative;
      overflow: hidden;
    }
    
    .sidenav-header::before {
      content: '';
      position: absolute;
      top: 0;
      right: 0;
      width: 30%;
      height: 100%;
      // background: linear-gradient(to left, rgba(26, 188, 156, 0.2), transparent);
      z-index: 1;
    }
    
    .logo-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      width: 100%;
      position: relative;
      z-index: 2;
    }
    
    .logo-wrapper {
      padding: 0 20px;
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;
    }
    
    .logo-image {
      width: 80%;
      height: 35px;
      object-fit: contain;
      filter: drop-shadow(0 2px 4px rgba(0, 0, 0, 0.1));
    }
    
    .header-divider {
      width: 40px;
      height: 3px;
      background-color: #1abc9c;
      margin-top: 8px;
      border-radius: 2px;
    }
    
    .logo-text {
      margin-left: 8px;
      font-size: 18px;
      font-weight: 500;
      color: #ffffff;
      text-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);
    }
    
    .sidenav-content {
      padding: 16px 0;
      flex: 1;
      overflow-y: auto;
    }
    
    .nav-section {
      margin-bottom: 24px;
    }
    
    .section-title {
      padding: 0 16px;
      margin-bottom: 8px;
      font-size: 12px;
      font-weight: 600;
      color: #7f8c8d;
      text-transform: uppercase;
      letter-spacing: 0.5px;
    }
    
    mat-nav-list {
      padding-top: 0;
    }
    
    mat-nav-list a {
      height: 44px;
      font-size: 14px;
      margin: 2px 8px;
      border-radius: 4px;
      transition: all 0.2s ease;
    }
    
    mat-nav-list a:hover {
      background-color: rgba(44, 62, 80, 0.05);
    }
    
    mat-nav-list a mat-icon {
      margin-right: 16px;
      color: #7f8c8d;
    }
    
    .active-link {
      background-color: rgba(26, 188, 156, 0.1) !important;
      color: #2c3e50;
      border-left: 3px solid #1abc9c;
    }
    
    .active-link mat-icon {
      color: #1abc9c !important;
    }
  `,
  ],
})
export class SidenavComponent {}