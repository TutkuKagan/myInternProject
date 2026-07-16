import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { environment } from '../../enviroments/environment.development';
import { IApiResponse } from '../../shared/models/api-response.model';
import { IAuthResponse, IUser } from '../../shared/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/auth`;
  private router = inject(Router);
  private currentUserSubject = new BehaviorSubject<IUser | null>(
    this.getUserFromStorage()
  );
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {}

  saveSession(authResponse: IAuthResponse): void {
    localStorage.setItem('token', authResponse.token);
    localStorage.setItem('tokenExpiration', authResponse.expiration);
    localStorage.setItem('currentUser', JSON.stringify(authResponse.user));
    
    this.currentUserSubject.next(authResponse.user);
  }

  clearSession(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('tokenExpiration');
    localStorage.removeItem('currentUser');
    
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUserFromStorage(): IUser | null {
    const userJson = localStorage.getItem('currentUser');
    return userJson ? JSON.parse(userJson) : null;
  }


  isAuthenticated(): boolean {
    const token = this.getToken();
    const expiration = localStorage.getItem('tokenExpiration');

    if (!token || !expiration) {
      return false;
    }

    const expirationDate = new Date(expiration).getTime();
    const currentTime = new Date().getTime();

    if (expirationDate < currentTime) {
      this.clearSession();
      return false;
    }

    return true;
  }


  login(credentials: any): Observable<IApiResponse<IAuthResponse>> {
    return this.http.post<IApiResponse<IAuthResponse>>(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: IApiResponse<IAuthResponse>) => {
        if (response.isSuccess && response.data) {
          this.saveSession(response.data);
        }
      })
    );
  }

  register(userData: any): Observable<IApiResponse<any>> {
    return this.http.post<IApiResponse<any>>(`${this.apiUrl}/register`, userData);
  }

  logout(): void {
    this.clearSession();
    this.router.navigate(['/auth/login']);
  }
}