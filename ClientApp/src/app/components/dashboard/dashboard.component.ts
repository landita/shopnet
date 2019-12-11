import { Component, OnInit, ViewChild } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';
import { ShoppingCartComponent } from '../shopping-cart/shopping-cart.component';
import { ShoppingCartService } from 'src/app/services/shopping-cart.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{

  @ViewChild(ShoppingCartComponent) child;
  user;
  invoice;
  constructor(
    private loginService:LoginService,
    private shoppingCartService:ShoppingCartService) { }

  ngOnInit() {
    this.user = this.loginService.getCurrentUSer();
    this.shoppingCartService.currentInvoice.subscribe(response => this.invoice = response);
  }

  logout(){
    this.loginService.logout();
  }

}
