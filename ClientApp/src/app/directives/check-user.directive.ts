import { Directive, Input } from '@angular/core';
import { NG_ASYNC_VALIDATORS, Validator, AbstractControl } from '@angular/forms';
import { LoginService } from '../services/login.service';
import { map } from 'rxjs/operators';

@Directive({
  selector: '[appCheckUser]',
  providers:[{
    provide:NG_ASYNC_VALIDATORS,
    useExisting:CheckUserDirective,
    multi:true
  }]
})
export class CheckUserDirective implements Validator{

  constructor(private loginService:LoginService) { }

  @Input() emailConfirm:string;
  validate(control:AbstractControl){
    const email = control.parent.get(this.emailConfirm);
    return this.loginService.checkUser({email:email.value, password:control.value})
      .pipe(map((response:boolean) => response ? {'checkUser':true} : null));
  }

}
