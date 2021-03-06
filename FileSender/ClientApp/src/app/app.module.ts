import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FileShareFormComponent } from './components/file-share-form/file-share-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NavbarComponent } from './components/navbar/navbar.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { DialogComponent } from './components/dialog/dialog.component';
import {MatIconModule} from '@angular/material/icon';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { FileDownloadComponent } from './components/file-download/file-download.component';
import {MatTableModule} from '@angular/material/table';
import { HighlightDirective } from './shared/directives/highlight.directive';
import { LoaderComponent } from './components/loader/loader.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'; 
import { LoaderService } from './shared/services/loader.service';
import { LoaderInterceptor } from './shared/services/interceptors/loader-interceptor.service';
import { ErrorInterceptor } from './shared/services/interceptors/error-interceptor.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    AppComponent,
    FileShareFormComponent,
    NavbarComponent,
    DialogComponent,
    FileDownloadComponent,
    HighlightDirective,
    LoaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    MatDialogModule,
    MatIconModule,
    ClipboardModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    FormsModule    
  ],
  providers: [
    LoaderService, 
    {provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
  ],
  entryComponents: [DialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
