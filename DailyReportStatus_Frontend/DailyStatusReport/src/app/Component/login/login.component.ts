import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Usersignin } from 'src/app/model/usersignin';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  userCredential= new Usersignin();
  loginForm: FormGroup
  constructor(private service:AuthService,private route:Router) {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)])
    });
  }

  ngOnInit(): void {
  }

  onSubmit() {

  }
  loginProceed() {
    if (this.loginForm.valid) {
      this.service.userLogin(this.loginForm.value).subscribe(
        (Response)=>{
           localStorage.setItem('Jwttoken',Response.jwtToken);
           this.route.navigateByUrl('/home');
        },
        (error)=>{
          if(error.error=="Please Confirm your Email"){
          this.route.navigateByUrl('/emailconfirm?'+"Email="+this.loginForm.value.email);
          }else{
          alert('Something Went Wrong');
        }}
      )
     // console.log(this.loginForm.value);
    }
    return this.loginForm.markAllAsTouched();
  }
}
