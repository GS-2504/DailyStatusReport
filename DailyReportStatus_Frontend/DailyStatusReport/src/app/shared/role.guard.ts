import { Injectable } from '@angular/core';
import {  CanActivate, Router} from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})

export class RoleGuard implements CanActivate {
  constructor(private service:AuthService,private route:Router){}
  canActivate(){
       this.service.haveAccess();

       return true;
  }
  
}
