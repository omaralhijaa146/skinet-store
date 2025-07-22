import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {NavigationExtras, Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router:Router,private toastr:ToastrService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).pipe(
      catchError((error:HttpErrorResponse) => {
        if(error){
          if(error.status === 400){
            if(error.error.errors){
              //throw error.error;
              return throwError(()=> error.error);
            }
            else {
              this.toastr.error(error.error.message, error.status.toString());
            }
          }

          if(error.status === 401){
            this.toastr.error(error.error.message,error.status.toString());
          }

          if (error.status === 404) {
            this.router.navigate(['/not-found']);
          }

          if(error.status === 500){
            const navigationExtras:NavigationExtras={
              state:{
                error:error.error
              }
            };
            this.router.navigate(['/server-error'],{
              state:navigationExtras
            });
          }
        }
        return throwError(()=> new Error(error.message));
      })
    );
  }
}
