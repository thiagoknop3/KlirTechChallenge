import { Product } from './product';
import { Promotion } from './promotion.enum';

export interface Item {
    product: Product;
    quantity: number;
    promotion: Promotion;
    total: number;
  }
