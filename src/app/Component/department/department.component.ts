import { Component ,OnInit} from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { DepartmentService } from '../../Services/department.service';
import { Department } from '../../Interfaces/Department';
import { AddDepartmentComponent } from './CreateDepartment/add-department/add-department.component';
import { AuthService } from '../../Services/auth.service';
@Component({
  selector: 'app-department',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent implements OnInit{
  Departments!:Department[];
  Name : any;
  Role : any;
  constructor( private deptService:DepartmentService , private router:Router , private authSrvice:AuthService) {}
  ngOnInit(): void {
    this.GetDepts();
    this.getNameFromToken();
  }
GetDepts(){
    this.deptService.GetDepartments().subscribe({
      next:(res:any)=>{
        console.log(res);
        this.Departments = res;
      } , error :(error:any)=>{
        console.log(error);
      }
    });
  }
  EditDept(data:any){

  }

  DeleteDept(id:any){
    this.deptService.DeleteDepartment(id).subscribe({
      next:(res:any)=>{
        console.log(res);
        this.GetDepts();
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

    }
  }
}
