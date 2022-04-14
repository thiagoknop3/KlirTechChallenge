import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Item } from '../models/item';
import { ItemCommand } from '../models/ItemCommand';
import { ShoppingCart } from '../models/shoppingCart';

@Component({
  selector: 'app-items-component',
  templateUrl: './items.component.html',
})

export class ItemsComponent {
  public shoppingCart: ShoppingCart;
  http: HttpClient;
  baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string) {
    http = http;
    baseUrl = baseUrl;
  }

  public AddProduct1() {
    const body: ItemCommand = { productId: 1, quantity: 1 };
    this.http.put<ShoppingCart>(this.baseUrl + 'shoppingCart/AddItem', body)
      .subscribe(data => this.shoppingCart = data);
  }
  public RemoveProduct1() {
    const body: ItemCommand = { productId: 1, quantity: 1 };
    this.http.put<ShoppingCart>(this.baseUrl + 'shoppingCart/RemoveItem', body)
      .subscribe(data => this.shoppingCart = data);
  }

  public AddProduct2() {
    const body: ItemCommand = { productId: 2, quantity: 1 };
    this.http.put<ShoppingCart>(this.baseUrl + 'shoppingCart/AddItem', body)
      .subscribe(data => this.shoppingCart = data);
  }
  public RemoveProduct2() {
    const body: ItemCommand = { productId: 2, quantity: 1 };
    this.http.put<ShoppingCart>(this.baseUrl + 'shoppingCart/RemoveItem', body)
      .subscribe(data => this.shoppingCart = data);
  }
}
