import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileDownloadComponent } from './components/file-download/file-download.component';
import { GuidResolver } from './components/file-download/resolvers/guid-resolver';
import { FileShareFormComponent } from './components/file-share-form/file-share-form.component';

const routes: Routes = [
  {path: "", component: FileShareFormComponent},
  {path:"files/:guid", component: FileDownloadComponent, resolve: {files: GuidResolver}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
