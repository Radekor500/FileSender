import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FileModel } from 'src/app/shared/models/fileModel';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private snackBar: MatSnackBar) { }

  showSnackbar(): void {
    this.snackBar.open("Copied to clipboard", "Close", {duration: 1000});
  }

  ngOnInit(): void {
    console.log(this.data);
  }

}
