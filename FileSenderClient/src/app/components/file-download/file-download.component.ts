import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { FileService } from 'src/app/shared/services/file.service';
import { FileDownloadModel } from 'src/app/shared/models/fileDownloadModel';
import { FileModel } from 'src/app/shared/models/fileModel';


@Component({
  selector: 'app-file-download',
  templateUrl: './file-download.component.html',
  styleUrls: ['./file-download.component.scss']
})
export class FileDownloadComponent implements OnInit {


  files!: FileDownloadModel[];
  displayedColumns: string[] = ['fileName', 'option'];
  constructor(private fileService: FileService, private route: ActivatedRoute) { }


  generateLink(response: any, filename: string): void {
    let dataType = response.type;
    let binaryData = [];
    binaryData.push(response);
    let downloadLink = document.createElement('a');
    downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, {type: dataType}));
    if (filename)
          downloadLink.setAttribute('download', filename);
          document.body.appendChild(downloadLink);
          downloadLink.click();
  }

  downloadFile(id: string, filename: string) {
    this.fileService.downloadSingleFile(id).subscribe(response => {
      // const blob = new Blob([resp], {type: 'application/pdf'});
      // const url = window.URL.createObjectURL(blob);
      // window.open(url);
      this.generateLink(response, filename);
      
    })
  }

  downloadAll() {
    let guid = this.route.snapshot.paramMap.get('guid')!;
    this.fileService.downloadAllFiles(guid).subscribe(response => {
      this.generateLink(response, "archive.zip");
    })
  }

  ngOnInit(): void {
    this.route.data.subscribe(resp => {
      this.files = resp['files'];
    })
  }

}
