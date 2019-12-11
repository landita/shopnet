import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  constructor(private loginService:LoginService, private router:Router) { }

  ngOnInit(){
    let token = localStorage.getItem('token');
    if(token) this.router.navigate(['']);
  }

  submit(form:NgForm){
    this.loginService.login({
      email:form.value.email,
      password:form.value.password
    });
  }
}

