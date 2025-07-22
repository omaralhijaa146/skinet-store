import { Component } from '@angular/core';
import {environment} from "../../../environmnts/environment";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {

  private baseUrl= environment.apiUrl;

  validationErrors:string[]=[];

  constructor(private http:HttpClient) { }

  get404Error(){
    this.http.get(`${this.baseUrl}products/42`).subscribe({
      next:(data)=>{
        console.log(data);
      },
      error:(error)=>{
        console.log(error);
      }
    });
  }

  get500Error(){
    this.http.get(`${this.baseUrl}buggy/servererror`).subscribe({
      next:(data)=>{
        console.log(data);
      },
      error:(error)=>{
        console.log(error);
      }
    });
  }

  get400Error(){
    this.http.get(`${this.baseUrl}buggy/badrequest`).subscribe({
      next:(data)=>{
        console.log(data);
      },
      error:(error)=>{
        console.log(error);
      }
    });
  }


  get400ValidationError(){
    this.http.get(`${this.baseUrl}products/llll`).subscribe({
      next:(data)=>{
        console.log(data);
      },
      error:(error)=>{
        console.log(error);
        this.validationErrors=error.errors;
      }
    });
  }



}
