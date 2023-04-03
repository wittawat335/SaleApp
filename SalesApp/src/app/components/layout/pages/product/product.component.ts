import { ProductDialogComponent } from './../../dialogs/product-dialog/product-dialog.component';
import { Product } from 'src/app/shared/interfaces/product';
import { ProductService } from './../../../../shared/services/product.service';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { MatPaginator } from '@angular/material/paginator';
import { UtilityService } from './../../../../shared/utility/utility.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'Name',
    'Category',
    'Stock',
    'Price',
    'isActive',
    'Actions',
  ];
  dataSource = new MatTableDataSource<Product>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(
    private dialog: MatDialog,
    private productService: ProductService,
    private utService: UtilityService
  ) {}

  GetTableList() {
    this.productService.GetList().subscribe({
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

  ngOnInit(): void {
    this.GetTableList();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  newProduct() {
    this.dialog
      .open(ProductDialogComponent, {
        disableClose: true,
      })
      .afterClosed()
      .subscribe((result) => {
        if (result === 'true') {
          this.GetTableList();
        }
      });
  }

  updateProduct(data: Product) {
    this.dialog
      .open(ProductDialogComponent, {
        disableClose: true,
        data: data,
      })
      .afterClosed()
      .subscribe((result) => {
        if (result === 'true') {
          this.GetTableList();
        }
      });
  }

  deleteProduct(data: Product) {
    Swal.fire({
      title: 'Do you want delete user?',
      text: data.name,
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Yes',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No',
    }).then((result) => {
      if (result.isConfirmed) {
        this.productService.Delete(data.productId).subscribe({
          next: (data) => {
            if (data.status) {
              this.utService.showAlert(data.message, 'success');
              this.GetTableList();
            } else {
              this.utService.showAlert(data.message, 'Error');
            }
          },
          error: (e) => {},
        });
      }
    });
  }
}
