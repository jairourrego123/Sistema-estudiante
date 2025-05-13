import { Component, inject, Input, OnInit } from "@angular/core"
import { CommonModule } from "@angular/common"
import { MatToolbarModule } from "@angular/material/toolbar"
import { MatIconModule } from "@angular/material/icon"
import { MatButtonModule } from "@angular/material/button"
import { MatMenuModule } from "@angular/material/menu"
import type { MatSidenav } from "@angular/material/sidenav"
import { Router, RouterModule } from "@angular/router"
import { MatDivider } from "@angular/material/list"
import { AuthService } from "../../../../core/adapters/auth.service"

@Component({
  selector: "app-header",
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatIconModule, MatButtonModule, MatMenuModule, RouterModule, MatDivider],
  template: `
    <mat-toolbar color="primary" class="header-toolbar">
      <button mat-icon-button (click)="sidenav.toggle()" class="menu-button">
        <mat-icon>menu</mat-icon>
      </button>
      
      
      
      <span class="spacer"></span>
      
      
      {{nombreUsuario}}

      <button mat-icon-button [matMenuTriggerFor]="userMenu" class="user-avatar">
        <img src="assets/avatar.png"  class="avatar-img">
      </button>
      <mat-menu #userMenu="matMenu">
        
        <mat-divider></mat-divider>
        <button mat-menu-item (click)="logout()">
          <mat-icon>exit_to_app</mat-icon>
          <span>Logout</span>
        </button>
      </mat-menu>
      
     
    </mat-toolbar>
  `,
  styles: [
    `
    .header-toolbar {
      background-color: white;
      color: #333;
      border-bottom: 1px solid #e9ecef;
      height: 64px;
    }
    
    .menu-button {
      margin-right: 16px;
    }
    
   
    
    .spacer {
      flex: 1 1 auto;
    }
    
    .user-avatar {
        
      margin: 0 1rem;
      justify-content: end;
      display: flex;
      align-items: center;
    }
    
    .avatar-img {
      width: 2.2rem;
      height: 2.5rem;
      border-radius: 50%;
      object-fit: cover;
    }
  `,
  ],
})
export class HeaderComponent implements OnInit {



  @Input() sidenav!: MatSidenav
  private router = inject(Router);
  private authService = inject(AuthService);

  nombreUsuario = "";
  ngOnInit() {
    this.nombreUsuario = this.authService.getUserFullName() ?? "";

    this.authService.user$.subscribe(user => {
      if (user) {
        this.nombreUsuario = `${user.given_name} ${user.family_name}`.trim();
      } else {
        this.nombreUsuario = "";
      }
    });
  }

  logout() {
    this.authService.logout()
    this.router.navigate(["/portal-acceso"])
  }
}


