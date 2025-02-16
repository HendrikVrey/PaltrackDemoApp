import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import {
  DxFormModule,
  DxValidatorModule,
  DxButtonModule,
  DxDataGridModule,
} from 'devextreme-angular';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { ListViewComponent } from './components/list-view/list-view.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ListViewComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    DxFormModule,
    DxValidatorModule,
    DxButtonModule,
    DxDataGridModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
