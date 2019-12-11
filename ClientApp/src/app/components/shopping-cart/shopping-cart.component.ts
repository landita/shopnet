import { Component, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { switchMap } from 'rxjs/operators';
import { ShoppingCartService } from 'src/app/services/shopping-cart.service';
import { LoginService } from 'src/app/services/login.service';
import { carts } from 'src/app/models/carts';
import { products } from 'src/app/models/products';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit, OnDestroy {

  user;
  carts:carts[];
  invoice;
  products;
  categories;
  subcription:Subscription;
  constructor(
    private productService:ProductService,
    private loginService:LoginService,
    private shoppingCartService:ShoppingCartService) { }

  ngOnInit() {
    this.subcription = this.productService.getCategories()
    .pipe(switchMap(response => {
      this.categories = response;
      return this.productService.getAll();
    })).subscribe(response => this.products = response);
    this.getCarts();
  }

  ngOnDestroy(){
    this.subcription.unsubscribe();
  }

  getCarts(){
    this.user = this.loginService.getCurrentUSer();
    this.shoppingCartService.retrieve(this.user.unique_name)
      .subscribe(response => {
        this.carts = response as carts[];
        this.invoice = this.carts
          .map(x => x.quantity).reduce((x, y) => x+y,0);
        this.shoppingCartService.traceInvoice(this.invoice);
      });
  }

  getQuantity(carts:carts[], product:products){
    if(!carts) return 0;
    const obj = carts.find(x => x.productId == product.id);
    return obj ? obj.quantity : 0;
  }

  addToCart(productId){
    this.shoppingCartService.add({
      userId:this.user.unique_name as number,
      productId:productId,
      quantity:0
    }).subscribe(response => this.ngOnInit());
  }

  deleteFromCart(productId){
    this.shoppingCartService.deleteFromCart({
      userId:this.user.unique_name as number,
      productId:productId
    }).subscribe(response => this.ngOnInit());
  }

  filterByCategory(event){
    this.productService.filterByCategories(event.target.value)
      .subscribe(response => this.products = response);
  }
}
