import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-emailconfirmation',
  templateUrl: './emailconfirmation.component.html',
  styleUrls: ['./emailconfirmation.component.scss']
})
export class EmailconfirmationComponent implements OnInit {
   email:string
  constructor(private routeparam:ActivatedRoute,private service:AuthService,private route:Router) {
   
   } 
  ngOnInit(): void {
              
  }
  resendEmail(){
    debugger
    this.routeparam.queryParams.subscribe((params) => {
      this.email = params['Email'];
       console.log(this.email)
    });
  }
}
