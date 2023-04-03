import { SaleDetailDialogComponent } from './../../dialogs/sale-detail-dialog/sale-detail-dialog.component';
import { UtilityService } from './../../../../shared/utility/utility.service';
import { SaleService } from './../../../../shared/services/sale.service';
import { Sale } from './../../../../shared/interfaces/sale';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';

import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';

export const MY_DATA_FORMAT = {
  parse: {
    dataInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-sale-history',
  templateUrl: './sale-history.component.html',
  styleUrls: ['./sale-history.component.css'],
  providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMAT }],
})
export class SaleHistoryComponent implements OnInit, AfterViewInit {
  formGroup: FormGroup;
  optionsSearch: any[] = [
    { value: 'Date', description: 'dueDate' },
    { value: 'Number', description: 'saleNumber' },
  ];

  displayedColumns: string[] = [
    'recordDate',
    'documentNumber',
    'paymentType',
    'total',
    'actions',
  ];

  dataSource = new MatTableDataSource<Sale>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private saleService: SaleService,
    private utService: UtilityService
  ) {
    this.formGroup = this.fb.group({
      search: ['Date'],
      saleNumber: [''],
      startDate: [''],
      endDate: [''],
    });

    this.formGroup.get('search')?.valueChanges.subscribe((value) => {
      this.formGroup.patchValue({
        saleNumber: '',
        startDate: '',
        endDate: '',
      });
    });
  }

  ngOnInit(): void {}
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  saleSearch() {
    let startDate: string = '';
    let endDate: string = '';

    if (this.formGroup.value.search === 'Date') {
      startDate = moment(this.formGroup.value.startDate).format('DD/MM/YYYY');
      endDate = moment(this.formGroup.value.endDate).format('DD/MM/YYYY');

      if (startDate === 'Invalid Date' || endDate === 'Invalid Date') {
        this.utService.showAlert('you must enter both dates?', 'Oops!');
        return;
      }
    }
    console.log(this.formGroup.value.search);
    console.log(this.formGroup.value.saleNumber);

    this.saleService
      .Record(
        this.formGroup.value.search,
        this.formGroup.value.saleNumber,
        startDate,
        endDate
      )
      .subscribe({
        next: (data) => {
          if (data.status) {
            this.dataSource.data = data.value;
          } else {
            this.utService.showAlert(data.message, 'Oops!');
          }
        },
        error: (e) => {},
      });
  }

  saleDetail(sale: Sale) {
    this.dialog.open(SaleDetailDialogComponent, {
      data: sale,
      disableClose: true,
      width: '700px',
    });
  }
}
