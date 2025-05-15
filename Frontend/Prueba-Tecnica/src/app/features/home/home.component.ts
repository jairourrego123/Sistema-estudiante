import { Component } from "@angular/core"
import { CommonModule } from "@angular/common"
import { RouterOutlet } from "@angular/router"
import { MatSidenavModule } from "@angular/material/sidenav"
import { MatToolbarModule } from "@angular/material/toolbar"
import { MatIconModule } from "@angular/material/icon"
import { MatButtonModule } from "@angular/material/button"
import { MatListModule } from "@angular/material/list"
import { SidenavComponent } from "./components/sidenav/sidenav.component"
import { HeaderComponent } from "./components/header/header.component"

@Component({
  selector: "app-root",
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    SidenavComponent,
    HeaderComponent,
  ],
  template: `
    <div class="app-container">
      <mat-sidenav-container class="sidenav-container">
        <mat-sidenav #sidenav mode="side" opened class="sidenav">
          <app-sidenav></app-sidenav>
        </mat-sidenav>
        <mat-sidenav-content>
          <app-header [sidenav]="sidenav"></app-header>
          <main class="content">
            <div class="content-container">
              <router-outlet></router-outlet>
            </div>
          </main>
        </mat-sidenav-content>
      </mat-sidenav-container>
    </div>
  `,
  styles: [
    `
    .app-container {
      display: flex;
      flex-direction: column;
      position: absolute;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
    }
    
    .sidenav-container {
      flex: 1;
    }
    
    .sidenav {
      width: 250px;
      background-color: #f8f9fa;
      border-right: 1px solid #e9ecef;
    }
    
    .content {
      padding: 20px;
      background-color: rgba(26, 188, 156, 0.1) !important;
      
      min-height: calc(100vh - 64px);
      display: flex;
      justify-content: center;
    }
    
    .content-container {
      width: 100%;
      max-width: 1200px;
    }
  `,
  ],
})
export class HomeComponent {
  title = "academico-service"
}
