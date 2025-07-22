import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavBarComponent} from "./nav-bar/nav-bar.component";
import {RouterLink, RouterLinkActive, RouterModule} from "@angular/router";
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {ErrorInterceptor} from "./interceptors/error.interceptor";
import {ToastrModule} from "ngx-toastr";
import { SectionHeaderComponent } from './section-header/section-header.component';
import {BreadcrumbModule} from "xng-breadcrumb";
import {LoadingInterceptor} from "./interceptors/loading.interceptor";
import {NgxSpinnerModule} from "ngx-spinner";



@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    SectionHeaderComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right',
      preventDuplicates:true,
    }),
    BreadcrumbModule,
    NgxSpinnerModule
  ],
  exports: [
    NavBarComponent,
    SectionHeaderComponent,
    NgxSpinnerModule
  ],
  providers:[
    {
      provide:HTTP_INTERCEPTORS,
      useClass:ErrorInterceptor,
      multi:true
    }
    ,{
      provide:HTTP_INTERCEPTORS,
      useClass:LoadingInterceptor,
      multi:true
    }
  ]
})
export class CoreModule { }
