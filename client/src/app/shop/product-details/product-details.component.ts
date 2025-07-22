import {Component, OnInit} from '@angular/core';
import {Product} from "../../shared/models/product.model";
import {ShopService} from "../shop.service";
import {ActivatedRoute, ActivatedRouteSnapshot} from "@angular/router";
import {BreadcrumbService} from "xng-breadcrumb";
import {BasketService} from "../../basket/basket.service";
import {take} from "rxjs";

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{

  product?:Product;
  quantity=1;
  quantityInBasket=0;
   constructor(private shopService:ShopService, private route:ActivatedRoute,private bcService:BreadcrumbService,private basketService:BasketService ){
     this.bcService.set('@productDetails',' ');
   }




  loadProduct():void{

    const id =this.route.snapshot.paramMap.get('id');

    if(!id) return;

     this.shopService.getProduct(+id).subscribe({
      next:(data:Product):void=>{
        this.product=data;
        this.bcService.set('@productDetails',data.name);
        this.basketService.basket$.pipe(take(1)).subscribe({
          next:(basket)=>{
            const item = basket?.items.find(i=>i.id===+id);
            if(item) this.quantity=item.quantity;
            this.quantityInBasket=this.quantity;

          }
        });
      },
      error:(error):void=>{
        console.log(error);
      }
     })
  }

  incrementQuantity():void{
     this.quantity++;
   }
   decrementQuantity():void{
     this.quantity--;
   }
   updateBasket():void{
     if(this.product){
       if(this.quantity>this.quantityInBasket){
         const itemsToAdd=this.quantity-this.quantityInBasket;
         this.quantityInBasket+=itemsToAdd;
         this.basketService.addItemToBasket(this.product,itemsToAdd);
       }else{
         const itemsToRemove=this.quantityInBasket-this.quantity;
         this.quantityInBasket-=itemsToRemove;
         this.basketService.removeItemFromBasket(this.product.id,itemsToRemove);
       }
     }
   }

   get buttonText(){
     return this.quantityInBasket===0?"Add to basket":"Update basket";
   }

    ngOnInit(): void {
     this.loadProduct();
    }

}
