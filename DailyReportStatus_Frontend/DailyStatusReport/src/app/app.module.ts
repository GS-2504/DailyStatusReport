import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Component/login/login.component';
import {ReactiveFormsModule} from '@angular/forms';
import { SignUpComponent } from './Component/sign-up/sign-up.component';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtinterceptorService } from './services/jwtinterceptor.service';
import { ConfirmemailComponent } from './Component/confirmemail/confirmemail.component';
import { ActivatedRoute } from '@angular/router';
import { ResetpasswordComponent } from './Component/resetpassword/resetpassword.component';
import { ForgotpasswordComponent } from './Component/forgotpassword/forgotpassword.component';
import { HomeComponent } from './Component/home/home.component';
import { EmailconfirmationComponent } from './Component/emailconfirmation/emailconfirmation.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignUpComponent,
    ConfirmemailComponent,
    ForgotpasswordComponent,
    ResetpasswordComponent,
    HomeComponent,
    EmailconfirmationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:JwtinterceptorService,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
