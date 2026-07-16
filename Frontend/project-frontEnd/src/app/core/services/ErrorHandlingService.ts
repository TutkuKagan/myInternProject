import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {
  

  handleHttpError(error: HttpErrorResponse): string {
    let errorMessage;

    if (error instanceof HttpErrorResponse) {
      switch (error.status) {
        case 400:
          errorMessage = error.error?.message || 'Check the spaces';
          break;
        case 401:
          errorMessage = 'Login again. Time exceeded';
          break;
        case 403:
          errorMessage = 'You dont have authorization for this.';
          break;
        case 404:
          errorMessage = 'Page could not be found.';
          break;
        case 500:
          errorMessage = 'Error with the server. Try again later';
          break;
        default:
          errorMessage = `There was an error (${error.status}): ${error.message}`;
          break;
      }
    }
    console.error('ERROR SERVICE', error);
    alert(errorMessage);

    return errorMessage;
  }
}