import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends DataService {

  constructor(http: HttpClient) {
    super('api/products', http);
  }

  getCategories(){
    return this.http.get('api/products/categories');
  }

  filterProducts(query){
    return this.http.get('api/products/filterProducts' + '/' + query);
  }

  filterByCategories(query){
    return this.http.get('api/products/filterByCategories' + '/' + query);
  }

}
