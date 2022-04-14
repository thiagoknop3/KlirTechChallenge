import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Promotion } from '../models/promotion.enum';
import { ShoppingCart } from '../models/shoppingCart';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html'
})
export class ShoppingCartComponent {
  public shoppingCart: ShoppingCart;
  public Promotion = Promotion;

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string) {
    http.get<ShoppingCart[]>(baseUrl + 'shoppingCart').subscribe(result => {
      this.shoppingCart = result[0];
    }, error => console.error(error));
  }
}







