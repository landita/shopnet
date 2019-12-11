import { Component } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css']
})
export class EmailConfirmationComponent{

  user;
  constructor(private loginService:LoginService) { }
  submit(form:NgForm){
    this.loginService.sendEmailConfirmation(form.value.email)
      .subscribe(response => alert('an email has been send to your email account.'));
    localStorage.setItem('temp', form.value.email);
  }
}
