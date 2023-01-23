import { HttpInterceptor,HttpRequest,HttpEvent,HttpHandler } from '@angular/common/http';
import { Injectable,Injector } from '@angular/core';
import { Observable} from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class JwtinterceptorService implements HttpInterceptor {
  constructor(private inject:Injector) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authService = this.inject.get(AuthService);
    let jwttoken = req.clone({
      setHeaders:{
      Authorization:'bearer'+authService.getToken()
      },
    });
     return next.handle(jwttoken);
  }
 
}
