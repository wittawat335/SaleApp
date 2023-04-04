import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');
    if (token) {
      const clonedReq = req.clone({
        headers: req.headers.set(
          'Authorization',
          'Bearer ' + localStorage.getItem('token')
        ),
      });
      return next.handle(clonedReq).pipe(
        tap(
          (succ) => {},
          (err) => {
            if (err.status == 401) {
              localStorage.removeItem('token');
              this.router.navigateByUrl('/login');
            }
          }
        )
      );
    } else {
      return next.handle(req);
    }
  }
}
