import { Component, OnInit } from '@angular/core';

import { Chart, registerables } from 'chart.js';
import { DashBoardService } from 'src/app/shared/services/dash-board.service';

Chart.register(...registerables);

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  totalIncome: string = '0';
  totalSale: string = '0';
  totalProduct: string = '0';

  constructor(private service: DashBoardService) {}

  showGraph(label: any[], data: any[]) {
    const chart = new Chart('myChart', {
      type: 'bar',
      data: {
        labels: label,
        datasets: [
          {
            label: 'test',
            data: data,
            backgroundColor: ['rgba(54,162,235,0.2)'],
            borderColor: ['rgba(54,162,235,1)'],
            borderWidth: 1,
          },
        ],
      },
      options: {
        maintainAspectRatio: false,
        responsive: true,
        scales: {
          y: {
            beginAtZero: true,
          },
        },
      },
    });
  }
  ngOnInit(): void {
    this.service.Summary().subscribe({
      next: (data) => {
        if (data.status) {
          this.totalIncome = data.value.totalIncome;
          this.totalSale = data.value.salesTotal;
          this.totalProduct = data.value.totalProduct;

          const arrayData: any[] = data.value.listSalesWeek;
          console.log(this.totalSale);

          const labelTemp = arrayData.map((value) => value.date);
          const dataTemp = arrayData.map((value) => value.total);
          console.log(labelTemp, dataTemp);
          this.showGraph(labelTemp, dataTemp);
        }
      },
      error: (e) => {},
    });
  }
}
