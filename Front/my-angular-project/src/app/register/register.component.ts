import { Component } from '@angular/core';
import { AuthserviceService } from '../services/authservice.service';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  userRegister:any={};
  constructor(private authService:AuthserviceService){}
  register(){
    this.authService.register(this.userRegister);
  }
}
