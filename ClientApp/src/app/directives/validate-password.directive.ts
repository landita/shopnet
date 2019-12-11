import { Directive, Input } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl } from '@angular/forms';

@Directive({
  selector: '[appValidatePassword]',
  providers:[{
    provide:NG_VALIDATORS,
    useExisting:ValidatePasswordDirective,
    multi:true
  }]
})
export class ValidatePasswordDirective implements Validator{

  @Input() passwordInput;
  constructor() { }
  validate(control:AbstractControl){
    const password = control.parent.get(this.passwordInput);
    return (password.value == control.value) ? {'confirmPassword':true} : null;
  }

}
