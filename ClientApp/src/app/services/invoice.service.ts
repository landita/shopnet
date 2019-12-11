import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService extends DataService {

  constructor(http:HttpClient) {
    super('api/invoice', http);
  }
  addStocks(id, list){
    return this.http.put('api/invoice/addStocks/' + id, list);
  }
  getInvoice(id){
    return this.http.get('api/invoice/invoice-details/' + id);
  }
}
