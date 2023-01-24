import { style } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Useremailconfirm } from 'src/app/model/useremailconfirm';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-confirmemail',
  templateUrl: './confirmemail.component.html',
  styleUrls: ['./confirmemail.component.scss']
})
export class ConfirmemailComponent implements OnInit {
    confirm:Useremailconfirm= new Useremailconfirm();
  constructor(private service:AuthService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.confirmEmail();
    }
   
    confirmEmail(){
    this.route.queryParams.subscribe((params) => {
      this.confirm.UserId =params['UserId']; 
      this.confirm.Code= params['code'];
     this.service.emailConfirmed(this.confirm).subscribe(
      (response)=>{
        
      },
      (error)=>{
        alert('hlo')
        console.log(error);
      }
     )
    });
  }
    }    //  this.auth.emailConfirmed().subscribe(
      //   ()=>{},
      //  ()=>{}
      // )
  

