import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../core/services/AuthService';
import { IUser } from '../../../shared/models/user.model';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navigation.html',
  styleUrl: './navigation.scss'
})
export class Navigation {
  private authService = inject(AuthService);

  currentUser$ = this.authService.currentUser$;

  onLogout(): void {
    this.authService.logout();
  }
}