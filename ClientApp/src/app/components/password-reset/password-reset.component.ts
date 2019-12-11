import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {

  constructor(
    private router:Router,
    private route:ActivatedRoute,
    private loginService:LoginService) { }

  ngOnInit() {
    const temp = localStorage.getItem('temp');
    if(!temp) this.router.navigate(['/login']);
  }
  submit(form:NgForm){
    const token = this.route.snapshot.paramMap.get('token');
    this.loginService.resetPassword(token, {
      email:JSON.parse(localStorage.getItem('temp')),
      password:form.value.password
    }).subscribe(response => {
        localStorage.removeItem('temp');
        this.router.navigate(['/login'])
      });
  }
}
