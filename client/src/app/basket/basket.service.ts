import { Injectable } from '@angular/core';
import {environment} from "../../environmnts/environment";
import {BehaviorSubject, Observable, tap} from "rxjs";
import {Basket, BasketTotals} from "../shared/models/basket.model";
import {HttpClient, HttpParams} from "@angular/common/http";
import {BasketItem} from "../shared/models/basket-item.model";
import {Product} from "../shared/models/product.model";
@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  private basketTotalSource = new BehaviorSubject<BasketTotals|null>(null);
  basketTotal$ = this.basketTotalSource.asObservable();
  basket$ = this.basketSource.asObservable();

  constructor(private http: HttpClient) {
  }


  getBasket(id: string) {
    return this.http.get<Basket>(`${this.baseUrl}basket`, {
      params: new HttpParams().append('id', id)
    }).subscribe({
      next: (data: Basket) => {
        this.basketSource.next(data);
        this.calculateBasketTotals();
      }
    });
  }

  setBasket(basket: Basket) {
    return this.http.post<Basket>(`${this.baseUrl}basket`, basket).subscribe({
      next: (data: Basket): void => {
        this.basketSource.next(data);
        this.calculateBasketTotals();
      }
    });
  }

  getCurrentBasket() {
    return this.basketSource.value;
  }

  addItemToBasket(item: Product|BasketItem, quantity: number = 1) {

    if (this.isProduct(item)) {
      item=this.mapProductItemToBasketItem(item);
    }

    const basket = this.getCurrentBasket() ?? this.createBasket();
    basket.items=this.addOrUpdateItem(basket.items, item, quantity);

    this.setBasket(basket);
}

removeItemFromBasket(id:number,quantity:number=1){
    const basket=this.getCurrentBasket();
    if(!basket) return;
    const item= basket.items.find(x=>x.id===id);

    if(item){
      item.quantity-=quantity;
      if(item.quantity<=0){
        basket.items=basket.items.filter(x=>x.id!==id);
      }
      if(basket.items.length>0){
        this.setBasket(basket);
      }else {
        this.deleteBasket(basket);
      }
    }

}

private isProduct(item:Product|BasketItem):item is Product{
  return (item as Product).productBrand !== undefined;
}

private calculateBasketTotals(){

    const basket= this.getCurrentBasket();
    if(!basket) return;
    const shippingCost=0;
    const subTotal=basket.items.reduce((acc:number,curr:BasketItem)=>(curr.price*curr.quantity)+acc,0);
    const total=subTotal+shippingCost;
    this.basketTotalSource.next({
      total,
      subtotal:subTotal,
      shipping:shippingCost
    });
}

  private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number):BasketItem[] {

    const item= items.find(x=>x.id===itemToAdd.id);

    if(item){
      item.quantity+=quantity;
    }
    else{
      itemToAdd.quantity=quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private createBasket():Basket{

    const basket= new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item:Product):BasketItem{

    return {
      id:item.id,
      productName:item.name,
      price:item.price,
      quantity:0,
      pictureUrl:item.pictureUrl,
      brand:item.productBrand,
      type:item.productType,
    };
  }


  private deleteBasket(basket: Basket) {

    return this.http.delete<boolean>(`${this.baseUrl}basket`,{
      params: new HttpParams().append('id', basket.id)
    }).subscribe({
      next: (data: boolean) => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      }
    });

  }
}
