import { Component, OnInit } from '@angular/core';
import { InvoiceService } from 'src/app/services/invoice.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  invoices;
  config;
  constructor(private invoiceService:InvoiceService) { }

  ngOnInit() {
    this.invoiceService.getAll()
      .subscribe(response => this.invoices = response);
    this.config = {
      itemsPerPage: 7,
      currentPage: 1,
    }
  }
  pageChanged(event){
    this.config.currentPage = event;
  }
}
