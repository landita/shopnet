import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DataService } from './data.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService extends DataService{

  private quantitySource = new BehaviorSubject<number>(0);
  currentInvoice = this.quantitySource.asObservable();
  constructor(http:HttpClient) {
    super('api/shoppingcart', http);
  }
  deleteFromCart(model){
    return this.http.delete(`api/shoppingcart/${model.userId}/${model.productId}`)
  }

  traceInvoice(total:number){
    this.quantitySource.next(total);
  }

  filterProduct(value){
    return this.http.get('api/shoppingcart' + '/' + value);
  }

  deleteAll(id){
    return this.http.delete('api/invoice/' + id);
  }

}
