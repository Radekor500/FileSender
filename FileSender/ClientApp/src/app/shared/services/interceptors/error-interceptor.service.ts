import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from "rxjs/operators";
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private snackBar: MatSnackBar) {
    }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        return next.handle(request)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    let errorMsg = '';
                    if (error.error instanceof ErrorEvent) {
                        errorMsg = `Error: ${error.error.message}`;
                    } else {
                        errorMsg = `Error Code: ${error.status},  Message: ${error.message}`;
                    }
                    console.log(errorMsg);
                    this.snackBar.open("Ups...Something went wrong", "Close");
                    return throwError(errorMsg);
                })
            )
    }
}