import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConfirmemailComponent } from './Component/confirmemail/confirmemail.component';
import { EmailconfirmationComponent } from './Component/emailconfirmation/emailconfirmation.component';
import { ForgotpasswordComponent } from './Component/forgotpassword/forgotpassword.component';
import { HomeComponent } from './Component/home/home.component';
import { LoginComponent } from './Component/login/login.component';
import { ResetpasswordComponent } from './Component/resetpassword/resetpassword.component';
import { SignUpComponent } from './Component/sign-up/sign-up.component';
import { AuthGuard } from './shared/auth.guard';

const routes: Routes = [
  {path:"",redirectTo:"home",pathMatch:"full"},
  {path:"home",component:HomeComponent},
  {path:"login",component:LoginComponent},
  {path:"signup",component:SignUpComponent},
  {path:"confirmemail",component:ConfirmemailComponent},
  {path:"emailconfirm",component:EmailconfirmationComponent},
  {path:"forgotpassword",component:ForgotpasswordComponent},
  {path:"resetpassword",component:ResetpasswordComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
