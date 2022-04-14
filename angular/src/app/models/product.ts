import { Promotion } from './promotion.enum';

export interface Product {
    id: number;
    name: string;
    price: number;
    promotion: Promotion;
  }
