import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Usersignin } from '../model/usersignin';
import { Useremailconfirm } from '../model/useremailconfirm';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http:HttpClient) { }

  userLogin(userCredentials:any):Observable<any>{
    return this.http.post<any>("https://localhost:44387/api/Account/UserSignIn",userCredentials);
  }
  userSignUp(userDetails:any):Observable<any>{
   return this.http.post<any>("https://localhost:44387/api/Account/RegisterUser",userDetails);
  }
  emailConfirmed(userIdandcode:Useremailconfirm):Observable<any>{
    return this.http.post<Useremailconfirm>("https://localhost:44387/api/Account/ConfirmEmail",userIdandcode);
  }
  isLoggedIn(){
     return localStorage.getItem('Jwttoken')!=null;
  }
  getToken(){
      return localStorage.getItem('JwtToken') || '';
  }
  haveAccess(){
     var JwtToken = this.getToken();
     var extractedToken = JwtToken.split('.')[1];
     var atobdata = atob(extractedToken);
     var finaldata = JSON.parse(atobdata);
  }
}

