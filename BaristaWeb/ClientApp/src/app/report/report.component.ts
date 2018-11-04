import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
})
export class ReportComponent {
  public beverageDistributions: BeverageDistn[];
  public orders: BeverageOrders[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<BeverageDistn[]>(baseUrl + 'api/pantry/00E01802-844E-4320-8A09-00E0574DA645/beverage/distribution').subscribe(result => {
      this.beverageDistributions = result;
    }, error => console.error(error));

    http.get<BeverageOrders[]>(baseUrl + 'api/pantry/00E01802-844E-4320-8A09-00E0574DA645/beverage/orders').subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }


}

interface BeverageDistn {
  name: string;
  dailyCount: number;
  monthlyCount: number;
}

interface BeverageOrders {
  name: string;
  date: string;
  quantity: number;
}
