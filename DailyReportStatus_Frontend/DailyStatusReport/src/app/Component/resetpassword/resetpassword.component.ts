import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Resetuserpassword } from 'src/app/model/resetuserpassword';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.scss']
})
export class ResetpasswordComponent implements OnInit {
  resetUserPassword: Resetuserpassword = new Resetuserpassword();
  form: FormGroup
  constructor(private service: AuthService, private route: ActivatedRoute,private routeurl:Router) {
    this.form = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    });
  }
  ngOnInit(): void {
    this.form.setValidators(this.matchPassword.bind(this));
  }
  resetPassword() {
    // console.log(this.form.value)
    if (this.form.invalid) return this.form.markAllAsTouched();
    this.route.queryParams.subscribe((params) => {
      this.resetUserPassword.UserId = params['UserId'];
      this.resetUserPassword.Token = params['code'];
      this.resetUserPassword.Password= this.form.value.password
      this.resetUserPassword.ConfirmPassword= this.form.value.confirmPassword
      console.log(this.resetUserPassword);
      this.service.resetPassword(this.resetUserPassword).subscribe(
        (response) => {
          this.routeurl.navigateByUrl('/login');
        },
        (error) => {
          alert('Something Went Wrong');
        }
      )
    });
  }
  matchPassword(formGroup: FormGroup) {
    const passwordControl = formGroup.get('password');
    const confirmPasswordControl = formGroup.get('confirmPassword');
    if (confirmPasswordControl.errors && !confirmPasswordControl.errors) {
      return;
    }
    if (passwordControl.value !== confirmPasswordControl.value) {
      confirmPasswordControl.setErrors({ mismatch: true });
    } else {
      confirmPasswordControl.setErrors(null);
    }
  }
}
