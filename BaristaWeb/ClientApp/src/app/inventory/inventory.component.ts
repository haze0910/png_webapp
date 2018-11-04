import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
})
export class InventoryComponent {
  public inventories: PantryInventory[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<PantryInventory[]>(baseUrl + 'api/pantry/00E01802-844E-4320-8A09-00E0574DA645/inventory').subscribe(result => {
      this.inventories = result;
    }, error => console.error(error));
  }

  
}

interface PantryInventory {
  name: string;
  quantity: number;
  unit: string;
}
