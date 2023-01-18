import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
 
  constructor(private service:AuthService,private route:Router) {
  }
  canActivate()
   {
    if(this.service.isLoggedIn()){
      return true;
    }
      this.route.navigate['/login'];
      return false;
  }
  
}
