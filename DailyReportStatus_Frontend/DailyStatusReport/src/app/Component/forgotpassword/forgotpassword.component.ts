import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.scss']
})
export class ForgotpasswordComponent implements OnInit {
  forgotpassword:FormGroup
  message:string
  showDiv:boolean= true;
  constructor(private service:AuthService) {
     this.forgotpassword= new FormGroup({
      email :new FormControl('',[Validators.required,Validators.email])
     })
   }

  ngOnInit(): void {
  }
  forgotPasswordEmail(){

     console.log(this.forgotpassword.value);
      if(this.forgotpassword.invalid)  return this.forgotpassword.markAllAsTouched();
        this.service.forgotPasswordEmailToUser(this.forgotpassword.value).subscribe(
          (response)=>{
             this.showDiv=false
             this.message="We Sent You A Reset Password Link on Your Email,Reset Your Password With the Link"
          },
          (error)=>{
            alert('You are not Registred with us');
          // console.log(error);
          }
        )
  }
}
