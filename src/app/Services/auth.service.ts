import { Injectable } from '@angular/core';
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private token: string | null = null;
  private decoded: { [key: string]: any } | null = null;

  constructor() {
    this.token = localStorage.getItem('token');
    if (this.token) {
      this.decodeToken();
    }
  }

  login(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    if (!this.token) {
        this.token = localStorage.getItem("token");
    }
    return this.token;
  }

  private decodeToken(): void {
    if (!this.token) {
      console.error("No token found.");
      return;
    }

    try {
      this.decoded = jwtDecode<{ [key: string]: any }>(this.token);
        console.log('Decoded Token:', this.decoded);
    } catch (error) {
      console.error("Invalid token:", error);
      this.decoded = null;
    }
  }


  getRole(): string | null {
    return this.decoded?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? null;

  }
  getName(): string | null {
    return this.decoded?.["UserName"] ??  null;
  }
  logout(): void {
    localStorage.clear();
}
}
