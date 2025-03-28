import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';

@Component({
  selector: 'app-root',
  imports: [RouterModule,LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Prueba-Tecnica';
}
