import { Component } from '@angular/core';
import {BasketService} from "../../basket/basket.service";
import {BasketItem} from "../../shared/models/basket-item.model";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(public basketService:BasketService){}

  getCount(item:BasketItem[]){
    return item.reduce((acc:number,curr:BasketItem):number=>acc+curr.quantity,0);
  }
}
