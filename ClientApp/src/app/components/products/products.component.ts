import { Component, OnInit, OnDestroy } from '@angular/core';
import { products } from 'src/app/models/products';
import { ProductService } from 'src/app/services/product.service';
import { switchMap } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit, OnDestroy {

  config:any;
  subscription:Subscription;
  products;
  categories;
  productModel:products = {
    id:0,
    name:'',
    category:'',
    price:null,
    imgUrl:null
  };
  cancel = 'none';

  constructor(
    private productService:ProductService,
    private toasr:ToastrService) {}

  ngOnInit() {
    this.subscription = this.productService
      .getAll()
      .pipe(switchMap(response => {
        this.products = response;
        return this.productService.getCategories();
      })).subscribe(response => this.categories = response);
      this.config = {
        itemsPerPage: 7,
        currentPage: 1,
      }
  }
  ngOnDestroy(){
    this.subscription.unsubscribe();
  }

  submit(productModel:products){
    if(productModel.id == 0) this.productService.add(productModel)
      .pipe(switchMap(response => {
        this.toasr.info('the record has been added', 'success');
        return this.productService.getAll()
      }))
      .subscribe(response => this.products = response);
    else this.productService.update(productModel.id, productModel)
      .pipe(switchMap(response => {
        this.toasr.info('the record has been updated', 'success');
        this.cancel = 'none';
        return this.productService.getAll();
      }))
      .subscribe(response => this.products = response);
  }

  clickBoard(product){
    this.cancel = 'block';
    this.productModel = Object.assign({}, product);
  }
  cancelButton(){
    this.cancel = 'none';
    this.productModel = {
      id:0,
      name:'',
      category:'',
      price:null,
      imgUrl:null
    };
  }
  deleteProduct(id){
    if(confirm("do you want to delete this record?")) 
      this.productService.delete(id)
        .pipe(switchMap(response => {
          this.toasr.info('the record has been removed', 'success');
          return this.productService.getAll();
        })).subscribe(response => this.products = response);
  }

  pageChanged(event){
    this.config.currentPage = event;
  }

  filterProducts(event){
    this.productService.filterProducts(event.target.value)
      .subscribe(response => this.products = response);
  } 
}
