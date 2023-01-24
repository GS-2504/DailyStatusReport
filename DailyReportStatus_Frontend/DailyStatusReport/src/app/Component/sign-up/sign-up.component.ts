import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
   signUpform:FormGroup
  constructor(private service:AuthService,private route:Router) {
    this.signUpform = new FormGroup({
      name:new FormControl('',[Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      confirmPassword: new FormControl('', [Validators.required])
    }); 
   }

  ngOnInit(): void {

    this.signUpform.setValidators(this.matchPassword.bind(this));
  }
  signUpProceed(){
    if(this.signUpform.valid){
      console.log()
     this.service.userSignUp(this.signUpform.value).subscribe(
      (response)=>{
      // alert("you are signup successfully");
        this.route.navigateByUrl('/emailconfirm');
      },
      (error)=>{
        alert('Something Went Wrong');
      }
     )
  }
        return this.signUpform.markAllAsTouched();
    }
  //custom
    matchPassword(formGroup: FormGroup) {
      const passwordControl = formGroup.get('password');
      const confirmPasswordControl = formGroup.get('confirmPassword');
      if (confirmPasswordControl.errors && !confirmPasswordControl.errors) {
        return;
      }
      if (passwordControl.value !== confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({ mismatch:true });
      } else {
        confirmPasswordControl.setErrors(null);
      }
    }
    
  }
