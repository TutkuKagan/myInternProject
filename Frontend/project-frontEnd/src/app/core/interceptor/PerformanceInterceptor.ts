import { HttpInterceptorFn } from '@angular/common/http';
import { tap } from 'rxjs/operators';

export const performanceInterceptor: HttpInterceptorFn = (req, next) => {
  const startTime = performance.now();

  return next(req).pipe(
    tap({
      finalize: () => {
        const elapsedTime = performance.now() - startTime;
        const durationFormatted = elapsedTime.toFixed(2);

        if (elapsedTime > 500) {
          console.warn(
            ` [HTTP SLOW REQUEST DETECTED] ${req.method} ${req.urlWithParams} -> ${durationFormatted} ms`
          );
        } else {
          console.log(
            ` [HTTP Performance] ${req.method} ${req.urlWithParams} -> ${durationFormatted} ms`
          );
        }
      }
    })
  );
};