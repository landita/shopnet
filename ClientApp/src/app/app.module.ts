import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from "@auth0/angular-jwt";
import { NgxPaginationModule } from 'ngx-pagination';
import { CustomFormsModule } from 'ng2-validation'
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { ProductsComponent } from './components/products/products.component';
import { LoginService } from './services/login.service';
import { CheckUserDirective } from './directives/check-user.directive';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProductService } from './services/product.service';
import { AuthGuardService } from './shared/auth-guard.service';
import { ToastrModule } from 'ngx-toastr';
import { JwtInterceptor } from './shared/jwt.interceptor';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { ShoppingCartService } from './services/shopping-cart.service';
import { EmailConfirmationComponent } from './components/email-confirmation/email-confirmation.component';
import { UserExistDirective } from './directives/user-exist.directive';
import { ValidatePasswordDirective } from './directives/validate-password.directive';
import { PasswordResetComponent } from './components/password-reset/password-reset.component';
import { InvoiceComponent } from './components/invoice/invoice.component';
import { InvoiceService } from './services/invoice.service';
import { OrdersComponent } from './components/orders/orders.component';
import { InvoiceDetailsComponent } from './components/invoice-details/invoice-details.component';

export function tokenGetter(){
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    ProductsComponent,
    CheckUserDirective,
    PageNotFoundComponent,
    ShoppingCartComponent,
    EmailConfirmationComponent,
    UserExistDirective,
    ValidatePasswordDirective,
    PasswordResetComponent,
    InvoiceComponent,
    OrdersComponent,
    InvoiceDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path:'' , component:DashboardComponent, children:[
        {path:'', redirectTo:'shopping-cart', pathMatch:'full'},
        {path:'shopping-cart', component:ShoppingCartComponent},
        {path:'invoice', component:InvoiceComponent},
        {path:'invoice-details/:id', component:InvoiceDetailsComponent},
        {path:'products', component:ProductsComponent},
        {path:'orders', component:OrdersComponent},
      ], canActivate:[AuthGuardService]},
      {path:'login', component:LoginComponent},
      {path:'email-confirmation', component:EmailConfirmationComponent},
      {path:'password-reset/:token', component:EmailConfirmationComponent},
      {path:'**', component:PageNotFoundComponent}
    ]),
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgbModule,
    JwtModule.forRoot({
      config:{
        tokenGetter:tokenGetter,
        whitelistedDomains:['localhost:5001'],
        blacklistedRoutes:[]
      }
    }),
    NgxPaginationModule,
    CustomFormsModule
  ],
  providers: [
    LoginService,
    ProductService,
    AuthGuardService,
    ShoppingCartService,
    InvoiceService,
    {
      provide:HTTP_INTERCEPTORS,
      useClass:JwtInterceptor,
      multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
