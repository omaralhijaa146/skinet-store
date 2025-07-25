import * as cuid from "cuid";
import {BasketItem} from "./basket-item.model";

export interface Basket {
  id:    string;
  items: BasketItem[];
}

export class Basket implements Basket{
  id= cuid();
  items: BasketItem[] = [];
}

export interface BasketTotals{
  shipping:number;
  subtotal:number;
  total:number;
}
