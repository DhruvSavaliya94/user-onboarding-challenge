// user-registration.component.ts

import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'user-registration',
  imports: [ReactiveFormsModule],
  templateUrl: './user-registration.component.html',
  styleUrl: './user-registration.component.css'
})
export class UserRegistrationComponent {
  userForm: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, this.phoneValidator]],
      address: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8), this.passwordValidator]]
    });
  }

  submitForm() {
    if (this.userForm.valid) {
      console.log("Form is valid");
      this.userService.addUser(this.userForm.value).subscribe(user => {
        //this.router.navigate(['/confirmation', user.id]);
      });
    }
  }

  phoneValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value || '';
    const regex = /^[0-9()+ -]+$/;
    return regex.test(value) ? null : { invalidPhone: true };
  }

  passwordValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value || '';
    const hasCapital = /[A-Z]/.test(value);
    const hasNumber = /[0-9]/.test(value);
    const hasSymbol = /[!@#$%^&]/.test(value);
    if (!hasCapital || !hasNumber || !hasSymbol) {
      return { passwordComplexity: true };
    }
    return null;
  }
}
