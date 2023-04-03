import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { ProductComponent } from './pages/product/product.component';
import { ReportComponent } from './pages/report/report.component';
import { SaleHistoryComponent } from './pages/sale-history/sale-history.component';
import { SaleComponent } from './pages/sale/sale.component';
import { UserComponent } from './pages/user/user.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'user', component: UserComponent },
      { path: 'product', component: ProductComponent },
      { path: 'sale', component: SaleComponent },
      { path: 'saleHistory', component: SaleHistoryComponent },
      { path: 'report', component: ReportComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LayoutRoutingModule {}
