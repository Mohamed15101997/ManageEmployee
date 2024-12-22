import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginObj: { userName: string; password: string } = {
    userName: '',
    password: '',
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService ,
    private fb:FormBuilder
  ) {}
  ngOnInit(): void {
    this.onLogin();
  }

  onLogin(): void {
    if (!this.loginObj.userName || !this.loginObj.password) {
      console.log('Please enter both username and password');
      return;
    }

    this.http
      .post('https://localhost:44332/api/Account/Login', this.loginObj)
      .subscribe({
        next: (response: any) => {
          if (response.result) {
            this.authService.login(response.token);
            this.router.navigateByUrl('dashboard');
          } else {
            console.log('Faild', response.result);
          }
        },
        error: (error: HttpErrorResponse) => {
          console.log(error);
        },
      });
  }
}
