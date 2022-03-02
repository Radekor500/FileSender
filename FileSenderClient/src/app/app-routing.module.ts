import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileShareFormComponent } from './components/file-share-form/file-share-form.component';

const routes: Routes = [
  {path: "", component: FileShareFormComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
