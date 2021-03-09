import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor // Đánh chặn lỗi
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';
import { BrowserStack } from 'protractor/built/driverProviders';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService)
  // router: we're going want to redirect the user to an error page
  // toastr: we want to just display a toast notification
  {

  }

  intercept(request:HttpRequest<unknown>,next: HttpHandler): Observable<HttpEvent<unknown>> {
    //HttpRequest: intercept that goes out
    //next: response that comes back in the next where we handle the response
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                // 3 lần error vì test lỗi 400 Validations, Ở console=>Info sẽ có 1 mảng error
                const modelStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modelStateErrors.push(error.error.errors[key])
                  }
                }
                throw modelStateErrors.flat(); // Nếu không có flat thì nó sẽ tạo ra 2 mảng lỗi khác nhau nhưng có cùng kiểu
                                              // trong Colsole của Browser
              }
              else {
                this.toastr.error(error.statusText, error.status)
              }
              break;

            case 401:
              this.toastr.error(error.statusText, error.status);
              break;

            case 404:
              this.router.navigateByUrl('/not-found');
              break;

            case 500:
              const NavigationExtras: NavigationExtras = { state: { error: error.error } };
              //state: this is going to be the exception that we get back from API
              this.router.navigateByUrl('server-error', NavigationExtras);
              break;

            default:
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }
        return throwError(error);
      }
      )

    )
  }
}
