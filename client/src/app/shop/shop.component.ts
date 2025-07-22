import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ShopService} from "./shop.service";
import {Product} from "../shared/models/product.model";
import {Pagination} from "../shared/models/pagination.model";
import {Brand} from "../shared/models/brand.model";
import {Type} from "../shared/models/type.model";
import {ShopParams} from "../shared/models/shop-params.model";


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{

  products:Product[]=[];
  brands:Brand[]=[];
  types:Type[]=[];

  shopParams=new ShopParams();

  sortOptions=[
    {name:'Alphabetical',value:'name'},
    {name:'Price: Low to high',value:'priceAsc'},
    {name:'Price: High to low',value:'priceDesc'},
  ];

  totalCount=0;

  @ViewChild('search') searchTerm?:ElementRef;

  constructor(private shopService:ShopService) { }

  onReset():void{
    if(this.searchTerm)
      this.searchTerm.nativeElement.value='';

    this.shopParams=new ShopParams();
    this.getProducts();
  }

  onSearch():void{
    this.shopParams.search=this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onPageChanged(pageNumber:number):void{
    if(this.shopParams.pageNumber===pageNumber) return;
      this.shopParams.pageNumber=pageNumber;
      this.getProducts();

  }

  onSortSelected(event:Event):void{
    this.shopParams.sort=(event.target as HTMLSelectElement).value;
    this.getProducts();
  }

  onBrandSelected(brandId:number):void{
    this.shopParams.brandId=brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onTypeSelected(typeId:number):void{
    this.shopParams.typeId=typeId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }


  ngOnInit(): void {
        this.getProducts();
        this.getBrands();
        this.getTypes();
    }

    getProducts():void{

      this.shopService.getProducts(this.shopParams).subscribe({
        next:(data:Pagination<Product[]>):void=>{
          this.products=data.data;
          this.shopParams.pageNumber=data.pageIndex;
          this.shopParams.pageSize=data.pageSize;
          this.totalCount=data.count;
        },
        error:(error):void=>{
          console.log(error);
        }
      });
    }

    getBrands():void{
      this.shopService.getBrands().subscribe({
        next:(data:Brand[]):void=>{
          this.brands=[{id:0,name:'All'},...data];
        },
        error:(error):void=>{
          console.log(error);
        }
      });
    }

    getTypes():void{
      this.shopService.getTypes().subscribe({
        next:(data:Type[]):void=>{
          this.types=[{id:0,name:'All'},...data];
        },
        error:(error):void=>{
          console.log(error);
        }
      });
    }

}
