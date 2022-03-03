import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { Observable, catchError, EMPTY } from "rxjs";
import { FileService } from "src/app/shared/services/file.service";
import { FileDownloadModel } from "src/app/shared/services/models/fileDownloadModel";

@Injectable({
    providedIn: 'root'
  })
  export class GuidResolver implements Resolve<any> {
  
    constructor(private fileService : FileService, private router : Router) {}
  
    resolve(route: ActivatedRouteSnapshot): Observable<FileDownloadModel> {
      return this.fileService.listAll(route.paramMap.get('guid')!).pipe(
          catchError(error => {
              alert("No uploads associated with provided link")
              this.router.navigate(["/"])
              return EMPTY;
            })
      )
  
      }
  
    }