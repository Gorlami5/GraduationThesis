import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http'
import { RegisterComponent } from '../register/register.component';
import { AppComponent } from '../app.component';


@NgModule({
  declarations: [
    RegisterComponent,
    AppComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
   

  ]
})
export class AppModule { }
