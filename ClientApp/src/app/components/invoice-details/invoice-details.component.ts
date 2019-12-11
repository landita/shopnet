import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InvoiceService } from 'src/app/services/invoice.service';
import { stocks } from 'src/app/models/stocks';

@Component({
  selector: 'app-invoice-details',
  templateUrl: './invoice-details.component.html',
  styleUrls: ['./invoice-details.component.css']
})
export class InvoiceDetailsComponent implements OnInit {


  invoice;
  invoiceId;
  constructor(
    private route:ActivatedRoute,
    private invoiceService:InvoiceService) { }

  ngOnInit() {
    this.invoiceId = this.route.snapshot.paramMap.get('id');
    this.invoiceService.getInvoice(this.invoiceId)
      .subscribe(response => this.invoice = response);
  }
  total(stocks:stocks[]){
    if(stocks){
      const total = stocks.map(x => x.totalPrice)
        .reduce((x, y) => x + y, 0);
        return total;
    }return 0;
  }
}
