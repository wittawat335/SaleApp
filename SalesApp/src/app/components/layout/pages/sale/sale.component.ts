import { Sale } from './../../../../shared/interfaces/sale';
import { UtilityService } from './../../../../shared/utility/utility.service';
import { SaleService } from './../../../../shared/services/sale.service';
import { ProductService } from './../../../../shared/services/product.service';
import { Product } from './../../../../shared/interfaces/product';
import { Component, ViewChild, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { SalesDetail } from 'src/app/shared/interfaces/sales-detail';

@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.css'],
})
export class SaleComponent implements OnInit {
  listProduct: Product[] = [];
  listProductFilter: Product[] = [];

  listProductForSale: SalesDetail[] = [];
  disableRegisterButton: boolean = false;

  productSelected!: Product;
  defaultPaymentType: string = 'cash';
  totalPay: number = 0;

  formProductSale: FormGroup;
  displayedColumns: string[] = [
    'product',
    'quantity',
    'price',
    'total',
    'action',
  ];
  dataSource = new MatTableDataSource(this.listProductForSale);

  returnProductsByFilter(search: any): Product[] {
    const value =
      typeof search === 'string'
        ? search.toLocaleLowerCase()
        : search.name.toLocaleLowerCase();

    return this.listProduct.filter((item) =>
      item.name.toLocaleLowerCase().includes(value)
    );
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private saleService: SaleService,
    private utService: UtilityService
  ) {
    this.formProductSale = this.fb.group({
      product: ['', Validators.required],
      quantity: ['', Validators.required],
    });

    this.productService.GetList().subscribe({
      next: (data) => {
        if (data.status) {
          const list = data.value as Product[];
          this.listProduct = list.filter((p) => p.isActive == 1 && p.stock > 0);
        }
      },
      error: (e) => {},
    });

    this.formProductSale.get('product')?.valueChanges.subscribe((value) => {
      this.listProductFilter = this.returnProductsByFilter(value);
    });
  }
  ngOnInit(): void {}

  getProductName(product: Product): string {
    return product.name;
  }

  productForSale(event: any) {
    this.productSelected = event.option.value;
  }

  addProductForSale() {
    const quantity: number = this.formProductSale.value.quantity;
    const price: number = parseFloat(this.productSelected.price);
    const total: number = quantity * price;
    this.totalPay = this.totalPay + total;
    this.listProductForSale.push({
      idProduct: this.productSelected.productId,
      productName: this.productSelected.name,
      quantity: quantity,
      priceText: String(price.toFixed(2)),
      totalText: String(total.toFixed(2)),
    });

    this.dataSource = new MatTableDataSource(this.listProductForSale);

    this.formProductSale.patchValue({
      product: '',
      quantity: '',
    });
  }

  deleteProduct(detail: SalesDetail) {
    (this.totalPay = this.totalPay - parseFloat(detail.totalText)),
      (this.listProductForSale = this.listProductForSale.filter(
        (p) => p.idProduct != detail.idProduct
      ));

    this.dataSource = new MatTableDataSource(this.listProductForSale);
  }

  registerSale() {
    if (this.listProductForSale.length > 0) {
      this.disableRegisterButton = true;

      const request: Sale = {
        paymentType: this.defaultPaymentType,
        totalText: String(this.totalPay.toFixed(2)),
        saleDetails: this.listProductForSale,
      };

      this.saleService.Register(request).subscribe({
        next: (data) => {
          if (data.status) {
            this.totalPay = 0.0;
            this.listProductForSale = [];
            this.dataSource = new MatTableDataSource(this.listProductForSale);

            Swal.fire({
              icon: 'success',
              title: 'Sale Register!',
              text: `Sale number: ${data.value.documentNumber}`,
            });
          } else {
            this.utService.showAlert('rrrrr', 'Oops');
          }
        },
        complete: () => {
          this.disableRegisterButton = false;
        },
        error: (e) => {},
      });
    }
  }
}
