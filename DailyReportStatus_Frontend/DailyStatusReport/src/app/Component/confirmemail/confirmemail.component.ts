import { style } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
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
  
  
    debugger
   this.route.queryParams.subscribe((params) => {
        this.confirm.UserId =params['UserId']; 
        this.confirm.Code= params['code'];
       this.service.emailConfirmed(this.confirm).subscribe(
        (response)=>{
           alert('hloo');
        },
        (error)=>{
          alert('error');
        }
       )
        //console.log(style); // OUTPUT 123
       // OUTPUT modular
       
      });
    }
    }
      //  this.auth.emailConfirmed().subscribe(
      //   ()=>{},
      //  ()=>{}
      // )
  

