import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Department } from '../Interfaces/Department';
import { Observable } from 'rxjs';
import { CreateDepartment } from '../Interfaces/CreateDepartment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  DepartmentApi = "https://localhost:44332/api/Departments";

  constructor(private http:HttpClient) { }

  GetDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.DepartmentApi);
  }
  createDepartment(model:CreateDepartment): Observable<CreateDepartment>{
      return this.http.post<CreateDepartment>(this.DepartmentApi,model);
  }
  DeleteDepartment(id:any){
    return this.http.delete(this.DepartmentApi + `/${id}`);
  }
  UpdateDepartment(id:any , model:Department){
    return this.http.put<Department>(this.DepartmentApi+ `/${id}`,model);
  }
  GetDepartmentByID(id:any): Observable<Department> {
    return this.http.get<Department>(this.DepartmentApi+ `/${id}`);
  }
}
