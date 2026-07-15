import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'; 
import { environment } from '../../enviroments/environment.development'; 
import { IApiResponse } from '../../shared/models/api-response.model';
import { IAuthResponse } from '../../shared/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient) {}


  login(credentials: any): Observable<IApiResponse<IAuthResponse>> {
    return this.http.post<IApiResponse<IAuthResponse>>(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: IApiResponse<IAuthResponse>) => { 
        if (response.isSuccess && response.data) {
          this.setToken(response.data.token);
          localStorage.setItem('currentUser', JSON.stringify(response.data.user));
        }
      })
    );
  }


  register(userData: any): Observable<IApiResponse<any>> {
    return this.http.post<IApiResponse<any>>(`${this.apiUrl}/register`, userData);
  }


  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
  }


  setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}