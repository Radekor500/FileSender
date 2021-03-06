import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/shared/services/loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {

  display!: boolean

  constructor(private loaderService: LoaderService) {

    this.loaderService.isLoading.subscribe((v) => {
      this.display = v;
    });
   }


  ngOnInit(): void {
  }

}
