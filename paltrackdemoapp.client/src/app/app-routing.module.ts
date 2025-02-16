import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'; 

import { LoginComponent } from './components/login/login.component';
import { ListViewComponent } from './components/list-view/list-view.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'list', component: ListViewComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
