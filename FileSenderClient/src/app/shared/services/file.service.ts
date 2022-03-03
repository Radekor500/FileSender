import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FileModel } from './models/fileModel';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) { }

  uploadFiles(data: FormData) : Observable<FileModel> {
    return this.http.post<FileModel>(`${environment.apiUrl}FileUpload/upload`, data);
  }

  downloadAllFiles(guid: string) : Observable<any> {
    const params = new HttpParams().set('guid', guid);
    return this.http.get<any>(`${environment.apiUrl}FileUpload/downloadall`, 
    {params: params, 
      responseType: "arraybuffer" as 'json'})
  }

  downloadSingleFile(guid: string) : Observable<any> {
    const params = new HttpParams().set('guid', guid);
    return this.http.get<any>(`${environment.apiUrl}FileUpload/downloadsingle`, 
    {params: params, 
      responseType: "blob" as 'json'})
  }

  listAll(guid: string) : Observable<FileModel> {
    const params = new HttpParams().set('guid', guid);
    return this.http.get<FileModel>(`${environment.apiUrl}FileUpload/upload`, {params: params});
  }
  
}
