import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface ToastMessage {
  id: number;
  type: 'success' | 'error' | 'info' | 'warning';
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private toastsSubject = new BehaviorSubject<ToastMessage[]>([]);
  toasts$ = this.toastsSubject.asObservable();

  showSuccess(message: string): void {
    this.addToast('success', message);
  }

  showError(message: string): void {
    this.addToast('error', message);
  }

  showInfo(message: string): void {
    this.addToast('info', message);
  }

  private addToast(type: 'success' | 'error' | 'info' | 'warning', message: string): void {
    const id = Date.now();
    const current = this.toastsSubject.value;
    this.toastsSubject.next([...current, { id, type, message }]);

    setTimeout(() => {
      this.removeToast(id);
    }, 3000);
  }

  removeToast(id: number): void {
    const updated = this.toastsSubject.value.filter(t => t.id !== id);
    this.toastsSubject.next(updated);
  }

  confirm(message: string): Promise<boolean> {
    return new Promise((resolve) => {
      const result = window.confirm(message);
      resolve(result);
    });
  }
}