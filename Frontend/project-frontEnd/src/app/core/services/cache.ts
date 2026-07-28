import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { shareReplay, tap } from 'rxjs/operators';

interface CacheEntry<T> {
  data$: Observable<T>;
  expiry: number;
}

@Injectable({
  providedIn: 'root'
})
export class CacheService {
  private cache = new Map<string, CacheEntry<any>>();

  /**
   * @param key
   * @param fallback$ 
   * @param ttlInMinutes 
   */
  get<T>(key: string, fallback$: Observable<T>, ttlInMinutes: number = 5): Observable<T> {
    const cached = this.cache.get(key);
    const now = Date.now();

    if (cached && cached.expiry > now) {
      return cached.data$;
    }

    const ttlMs = ttlInMinutes * 60 * 1000;
    const shared$ = fallback$.pipe(
      shareReplay(1),
      tap({
        error: () => this.clearKey(key)
      })
    );

    this.cache.set(key, {
      data$: shared$,
      expiry: now + ttlMs
    });

    return shared$;
  }

  clearKey(key: string): void {
    this.cache.delete(key);
  }

  clearAll(): void {
    this.cache.clear();
  }
}