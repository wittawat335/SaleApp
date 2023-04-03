import { Sale } from './../../../../shared/interfaces/sale';
import { Component, OnInit, Inject } from '@angular/core';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SalesDetail } from 'src/app/shared/interfaces/sales-detail';

@Component({
  selector: 'app-sale-detail-dialog',
  templateUrl: './sale-detail-dialog.component.html',
  styleUrls: ['./sale-detail-dialog.component.css'],
})
export class SaleDetailDialogComponent implements OnInit {
  recordDate: string = '';
  documentNumber: string = '';
  paymentType: string = '';
  total: string = '';
  saleDetail: SalesDetail[] = [];
  displayedColumns: string[] = ['product', 'quantity', 'price', 'total'];

  constructor(@Inject(MAT_DIALOG_DATA) public sale: Sale) {
    this.recordDate = sale.recordDate!;
    this.documentNumber = sale.documentNumber!;
    this.paymentType = sale.paymentType;
    this.total = sale.totalText;
    this.saleDetail = sale.saleDetails;
  }

  ngOnInit(): void {}
}
