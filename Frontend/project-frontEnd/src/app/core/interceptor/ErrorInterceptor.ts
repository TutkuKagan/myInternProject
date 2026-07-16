import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorHandlingService } from '../services/ErrorHandlingService';
import { AuthService } from '../services/AuthService';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const errorService = inject(ErrorHandlingService);
  const authService = inject(AuthService);
  const router = inject(Router);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {

      const processedMessage = errorService.handleHttpError(error);
    
      if (error.status === 401) {
        authService.logout();
        router.navigate(['/auth/login']);
      }

      return throwError(() => new Error(processedMessage));
    })
  );
};