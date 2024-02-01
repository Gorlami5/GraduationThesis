import { Injectable } from '@angular/core';
import { Login } from '../models/login';
import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Register } from '../models/register';

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {

  constructor(private httpClient:HttpClient) { }
  path = "http://localhost:5164/api/Auth";
  userToken:any;
  login(login:Login){
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type","application/json");
    this.httpClient.post(this.path + "Login",login,{headers:headers}).subscribe(data =>{
      this.saveToken(data.hasOwnProperty('tokenString') ? data : null)
      this.userToken = data;
    });
  }
  saveToken(token:any){
          localStorage.setItem('token',token);
  }
  register(register:Register){
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type","application/json");
    this.httpClient.post(this.path + "Register",register,{headers:headers}).subscribe(data =>{

    });
  }
}
