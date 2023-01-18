import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Usersignin } from '../model/usersignin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http:HttpClient) { }

  userLogin(userCredentials:any):Observable<any>{
    return this.http.post<any>("https://localhost:44387/api/Account/UserSignIn",userCredentials);
  }
  isLoggedIn(){
     return localStorage.getItem('Jwttoken')!=null;
  }
}

