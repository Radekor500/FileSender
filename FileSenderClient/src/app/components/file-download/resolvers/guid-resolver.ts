import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { Observable, catchError, EMPTY } from "rxjs";
import { FileService } from "src/app/shared/services/file.service";
import { FileDownloadModel } from "src/app/shared/models/fileDownloadModel";
import { DialogComponent } from "../../dialog/dialog.component";
import { MatDialog } from "@angular/material/dialog";

@Injectable({
    providedIn: 'root'
  })
  export class GuidResolver implements Resolve<any> {
  
    constructor(private fileService : FileService, private router : Router, private dialog: MatDialog) {}
  
    resolve(route: ActivatedRouteSnapshot): Observable<FileDownloadModel> {
      return this.fileService.listAll(route.paramMap.get('guid')!).pipe(
          catchError(error => {
            this.dialog.open(DialogComponent, {
              data: {
                title: "Something went wrong",
                content: "Link has either expired or is invalid",
                copy: false
              }
            })
              this.router.navigate(["/"])
              return EMPTY;
            })
      )
  
      }
  
    }