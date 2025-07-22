import {Component, Input, OnInit} from '@angular/core';
import {Product} from "../../shared/models/product.model";
import {BasketService} from "../../basket/basket.service";
import {add} from "ngx-bootstrap/chronos";

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit{

  @Input() product?:Product;
  constructor(private basketService:BasketService) { }

  addItemToBasket():void{
    this.product&&this.basketService.addItemToBasket(this.product);
  }

  ngOnInit(): void {

  }

  protected readonly add = add;
}
