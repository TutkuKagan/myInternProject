import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private readonly THEME_KEY = 'app-theme';
  
  isDarkMode = signal<boolean>(false);

  constructor() {
    this.initTheme();
  }

  private initTheme(): void {
    const savedTheme = localStorage.getItem(this.THEME_KEY);
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    if (savedTheme === 'dark' || (!savedTheme && prefersDark)) {
      this.setDarkMode(true);
    } else {
      this.setDarkMode(false);
    }
  }

  toggleTheme(): void {
    this.setDarkMode(!this.isDarkMode());
  }

  private setDarkMode(isDark: boolean): void {
    this.isDarkMode.set(isDark);
    
    if (isDark) {
      document.body.classList.add('dark-theme');
      localStorage.setItem(this.THEME_KEY, 'dark');
    } else {
      document.body.classList.remove('dark-theme');
      localStorage.setItem(this.THEME_KEY, 'light');
    }
  }
}