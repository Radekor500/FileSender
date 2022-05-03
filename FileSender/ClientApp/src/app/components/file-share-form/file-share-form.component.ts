import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
  date: Date = new Date();
  constructor(private fileService: FileService, private dialog: MatDialog, private fb: FormBuilder) { }

  fileForm: FormGroup = this.fb.group({
    uploadFileName: ["Choose files", Validators.required],
    expiryDate: [null]
  })

  onFileChange(e: any) {

    if (e.target.files && e.target.files[0]) {

      let fileStr = '';
      Array.from(e.target.files).forEach((file: any) => {
        fileStr += file.name + ',';
      });
      this.fileForm.controls['uploadFileName'].patchValue(fileStr);
    }

    // let fake = document.getElementById('fake') as any;
    // let real = document.getElementById('fileControlInput') as any;
    // console.log('fake', fake.files);
    // console.log('real', real.files);
    console.log(this.uploadControl.nativeElement.files)
  }

  onSubmit() {
    console.log(this.fileForm.valid);
    const formData = new FormData();
    let files = this.uploadControl.nativeElement.files;
    Object.keys(files).forEach(key => {
      console.log(files[key])
      formData.append("FileContent", files[key]);
    });
    formData.append("ExpiryDate", new Date(this.fileForm.controls['expiryDate'].value).toISOString());

    this.fileService.uploadFiles(formData).subscribe(resp => {
      console.log(resp);
      this.dialog.open(DialogComponent, {
        data: {
          content: `${environment.shareUrl}${resp.uploadId}`,
          title: "Here's your link to share files:",
          copy: true
        }
      })
      this.fileForm.reset();
    }
    )
  }

  ngOnInit(): void {
  }

}
