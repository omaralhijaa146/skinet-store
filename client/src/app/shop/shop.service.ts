
import {Injectable, OnInit} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Pagination} from "../shared/models/pagination.model";
import { Product } from '../shared/models/product.model';
import { Observable } from 'rxjs';
import {Brand} from "../shared/models/brand.model";
import {Type} from "../shared/models/type.model";
import {ShopParams} from "../shared/models/shop-params.model";

@Injectable({
  providedIn: 'root'
})
export class ShopService implements OnInit {

  constructor(private http:HttpClient) { }

  baseUrl='https://localhost:5001/api/';

  getProduct(id:number):Observable<Product>{
    return this.http.get<Product>(`${this.baseUrl}products/${id}`);
  }


  getProducts(shopParams:ShopParams):Observable<Pagination<Product[]>>{

    let params= new HttpParams();

    if(shopParams.brandId>0){
      params=params.append('brandId',shopParams.brandId.toString());
    }
    if(shopParams.typeId>0){
      params=params.append('typeId',shopParams.typeId.toString());
    }

      params=params.append('sort',shopParams.sort);
      params=params.append('pageIndex',shopParams.pageNumber);
      params=params.append('pageSize',shopParams.pageSize);

      if(shopParams.search)
        params=params.append('search',shopParams.search)

    return this.http.get<Pagination<Product[]>>(`${this.baseUrl}products`,{
      params
    });
  }

  getBrands():Observable<Brand[]>{
    return this.http.get<Brand[]>(`${this.baseUrl}products/brands`);
  }

  getTypes():Observable<Type[]>{
    return this.http.get<Brand[]>(`${this.baseUrl}products/types`);
  }

  ngOnInit(): void {

    }


}
