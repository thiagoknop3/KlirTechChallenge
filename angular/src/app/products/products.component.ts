import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Promotion } from '../models/promotion.enum';
import { ShoppingCart } from '../models/shoppingCart';
import { Product } from '../models/product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent {
  public products: Product[];
  public Promotion = Promotion;

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string) {
    http.get<Product[]>(baseUrl + 'products').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }
}







