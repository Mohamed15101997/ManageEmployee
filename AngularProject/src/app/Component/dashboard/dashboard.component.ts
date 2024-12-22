import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  Name : any;
  Role : any;
  constructor(private authSrvice:AuthService) {}
  ngOnInit(): void {
    this.getNameFromToken();
  }
  getNameFromToken(){
    const token = this.authSrvice.getToken();
    if(token != null){
      this.Name = this.authSrvice.getName()
      this.Role = this.authSrvice.getRole()
    }
  }
  Logout(){
    this.authSrvice.logout();
  }


}
