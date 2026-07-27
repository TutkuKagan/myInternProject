import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from './core/services/AuthService';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  private authService = inject(AuthService);
  private router = inject(Router);

  isDarkMode = false;

  ngOnInit(): void {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
      this.isDarkMode = true;
      document.body.classList.add('dark-theme');
    }
  }

  toggleTheme(): void {
    this.isDarkMode = !this.isDarkMode;
    document.body.classList.toggle('dark-theme', this.isDarkMode);
    localStorage.setItem('theme', this.isDarkMode ? 'dark' : 'light');
  }

  onLogout(): void {
    localStorage.removeItem('token');
    
    if (typeof this.authService.logout === 'function') {
      this.authService.logout();
    }

    this.router.navigate(['/login']);
  }
}