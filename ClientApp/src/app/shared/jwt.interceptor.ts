import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class JwtInterceptor implements HttpInterceptor{

  constructor(private router:Router, private jwt:JwtHelperService) { }

  intercept(request:HttpRequest<any>, next:HttpHandler):Observable<HttpEvent<any>>{
    const token = localStorage.getItem('token');
    if(token){
      const clone = request.clone({
        setHeaders:{
          Authorization:'Bearer ' + token
        }
      })
      return next.handle(clone).pipe(tap(
        success => {},
        error => {
          if(error.status == 401){
            localStorage.removeItem('token');
            this.router.navigate(['login']);
          }
          else if(error.status == 403) this.router.navigate(['**'])
        }
      ));
    }
    return next.handle(request.clone());
  }
}
