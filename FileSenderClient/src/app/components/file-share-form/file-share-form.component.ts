import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { FileService } from 'src/app/shared/services/file.service';
import { environment } from 'src/environments/environment';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-file-share-form',
  templateUrl: './file-share-form.component.html',
  styleUrls: ['./file-share-form.component.scss']
})
export class FileShareFormComponent implements OnInit {

  @ViewChild('uploadControl') uploadControl!: ElementRef;
  uploadFileName = 'Choose File';
  expiryDate = new FormControl(null)
  constructor(private fileService: FileService, private dialog: MatDialog) { }

  onFileChange(e: any) {

    if (e.target.files && e.target.files[0]) {

      this.uploadFileName = '';
      Array.from(e.target.files).forEach((file: any) => {
        this.uploadFileName += file.name + ',';
      });
    }

    // let fake = document.getElementById('fake') as any;
    // let real = document.getElementById('fileControlInput') as any;
    // console.log('fake', fake.files);
    // console.log('real', real.files);
    console.log(this.uploadControl.nativeElement.files)
  }

  onSubmit() {
    const formData = new FormData();
    let files = this.uploadControl.nativeElement.files;
    Object.keys(files).forEach(key => {
      console.log(files[key])
      formData.append("FileContent", files[key]);
    });
    formData.append("ExpiryDate", new Date(this.expiryDate.value).toISOString());

    this.fileService.uploadFiles(formData).subscribe(resp => {
      console.log(resp);
      this.dialog.open(DialogComponent, {
        data: {
          uploadId: `${environment.shareUrl}${resp.uploadId}`
        }
      })
    })
  }

  ngOnInit(): void {
  }

}
