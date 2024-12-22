import { Component ,OnInit} from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CreateUserService } from '../../Services/user.service';
import { User } from '../../Interfaces/User';
import { GetUser } from '../../Interfaces/GetUsers';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent implements OnInit{
  Name : any;
  Role : any;
  constructor( private userService:CreateUserService , private router:Router ,  private authSrvice:AuthService) {}
  Employess!:GetUser[];
  ngOnInit(): void {
    this.GetEmps();
  }
  GetEmps(){
      this.userService.GetUsers().subscribe({
        next:(res:any)=>{
          console.log(res);
          this.Employess = res
        } , error :(error:any)=>{
          console.log(error);
        }
      });
    }
    DeleteUser(id:any){
      this.userService.DeleteUser(id).subscribe({
        next:(res:any)=>{
          console.log(res);
          this.GetEmps();
        },
        error:(error:any)=>{}
      })
    }
    getNameFromToken(){
    const token = this.authSrvice.getToken();
    if(token != null){
      this.Name = this.authSrvice.getName()
      console.log(this.Name = this.authSrvice.getName());

      this.Role = this.authSrvice.getRole()
      console.log(this.Role = this.authSrvice.getRole());

    }}


}
