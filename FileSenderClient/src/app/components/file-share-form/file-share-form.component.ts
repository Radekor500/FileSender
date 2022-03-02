import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-file-share-form',
  templateUrl: './file-share-form.component.html',
  styleUrls: ['./file-share-form.component.scss']
})
export class FileShareFormComponent implements OnInit {

  @ViewChild('uploadControl') uploadControl!: ElementRef;
  uploadFileName = 'Choose File';
  expiryDate = new FormControl(null)
  constructor() { }

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
      formData.append("FileContnet", files[key]);
    });
    formData.append("ExpiryDate", this.expiryDate.value);
    console.log(this.expiryDate.value)
    console.log(formData);
  }

  ngOnInit(): void {
  }

}
