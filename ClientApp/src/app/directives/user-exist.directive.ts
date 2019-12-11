import { Directive } from '@angular/core';
import { NG_ASYNC_VALIDATORS, Validator, AbstractControl } from '@angular/forms';
import { LoginService } from '../services/login.service';
import { map } from 'rxjs/operators';

@Directive({
  selector: '[appUserExist]',
  providers:[{
    provide:NG_ASYNC_VALIDATORS,
    useExisting:UserExistDirective,
    multi:true
  }]
})
export class UserExistDirective implements Validator {

  constructor(private loginService:LoginService) { }
  validate(control:AbstractControl){
    return this.loginService.userExists(control.value)
      .pipe(map((response:boolean) => response ? {'userNotExists':true} : null));
  }

}
