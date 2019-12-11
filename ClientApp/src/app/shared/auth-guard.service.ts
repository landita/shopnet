import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CanActivate } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate{

  constructor(private router:Router, private jwt:JwtHelperService) { }
  canActivate(){
    let token = localStorage.getItem('token');
    if(token && !this.jwt.isTokenExpired(token)) return true;
    this.router.navigate(['login']);
    return false;
  }
}
