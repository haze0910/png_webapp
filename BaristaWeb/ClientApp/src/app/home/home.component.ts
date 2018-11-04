import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Headers, RequestOptions } from '@angular/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  public beverages: BeverageDTO[];
  public vendResult: any;
  private localUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.localUrl = baseUrl; 
  }

  ngOnInit() {
    this.fetchData();
  }

  private fetchData() {
    this.http.get<BeverageDTO[]>(this.localUrl + 'api/pantry/00E01802-844E-4320-8A09-00E0574DA645/beverages').subscribe(result => {
      this.beverages = result;
    }, error => console.error(error));
  }

  public getThis(data:any) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
   
    this.http.post(this.localUrl + 'api/pantry/00E01802-844E-4320-8A09-00E0574DA645/beverage/'+ data.beverageId + '/vend', headers).subscribe(result => {
      this.vendResult = result;
      alert("Getting " + data.name + " was successful.");
    }, error =>
    {
      alert("Error in getting " + data.name + " : " + error.error)
    }
    );
  }
}

interface BeverageDTO {
    beverageId: string;
    name: string;
    description: string;
    image: string;
  
}
