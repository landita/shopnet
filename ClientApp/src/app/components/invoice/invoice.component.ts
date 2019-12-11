import { Component, OnInit } from '@angular/core';
import { invoice } from 'src/app/models/invoice';
import { LoginService } from 'src/app/services/login.service';
import { InvoiceService } from 'src/app/services/invoice.service';
import { switchMap } from 'rxjs/operators';
import { ShoppingCartService } from 'src/app/services/shopping-cart.service';
import { Router } from '@angular/router';
import { stocks } from 'src/app/models/stocks';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {

  config;
  carts:stocks[];
  user;
  invoice:invoice = {
    id:0,
    address:'',
    cardName:'',
    cardNumber:'',
    expiration:'',
    cvv:'',
    userId:0
  }
  constructor(
    private loginService:LoginService,
    private invoiceService:InvoiceService,
    private shoppingCartService:ShoppingCartService,
    private router:Router) { }

  ngOnInit() {
    this.user = this.loginService.getCurrentUSer();
    this.invoice.userId = Number(this.user.unique_name);
    this.invoiceService.retrieve(this.invoice.userId)
      .subscribe(response => this.carts = response as stocks[]);
    this.config = {
      itemsPerPage: 6,
      currentPage: 1,
    }
  }

  submit(carts:stocks[], invoice:invoice){
    this.invoiceService.add(invoice)
      .pipe(switchMap(() => this.invoiceService.addStocks(invoice.userId, carts)))
      .pipe(switchMap(() => this.shoppingCartService.deleteAll(this.user.unique_name)))
      .subscribe(() => this.router.navigate(['', 'shopping-cart']));
  }
  pageChanged(event){
    this.config.currentPage = event;
  }

}
