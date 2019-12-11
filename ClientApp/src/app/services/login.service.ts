import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private http:HttpClient, 
    private router:Router, 
    private jwt:JwtHelperService) { }

  login(model){
    this.http.post('api/login', model)
      .subscribe((response:any) => {
        localStorage.setItem('token', response.token);
        this.router.navigate([''])
      });
  }
  checkUser(model){
    return this.http.post('api/login/checkUserExists', model);
  }
  logout(){
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }

  getCurrentUSer(){
    const token = localStorage.getItem('token');
    const obj = this.jwt.decodeToken(token);
    return obj;
  }

  sendEmailConfirmation(email){
    return this.http.get('api/login/sendEmailConfirmation/' + email);
  }

  userExists(email){
    return this.http.get('api/login/userExists/' + email);
  }

  resetPassword(token, model){
    return this.http.put('api/login/resetPassword/' + token, model);
  }
}
