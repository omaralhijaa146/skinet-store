import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {delay, finalize, Observable, tap} from 'rxjs';
import {BusyService} from "../services/busy.service";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService:BusyService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    this.busyService.busy();

    return next.handle(request).pipe(delay(500),finalize(()=>this.busyService.idle()));
  }
}
