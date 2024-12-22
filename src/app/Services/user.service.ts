import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../Interfaces/User';
import { Observable } from 'rxjs';
import { GetUser } from '../Interfaces/GetUsers';
import { GetUserById } from '../Interfaces/GetUserById';

@Injectable({
  providedIn: 'root'
})
export class CreateUserService {
  AuthRegisterApi = "https://localhost:44332/api/Account/Register";
  UsersApi = "https://localhost:44332/api/Users";

  constructor(private http:HttpClient) { }
  createAccount(model:User): Observable<User>{
    return this.http.post<User>(this.AuthRegisterApi,model);
  }
  GetUsers(): Observable<GetUser[]>{
    return this.http.get<GetUser[]>(this.UsersApi);
  }
  GetEmployeeByID(id:any): Observable<GetUserById> {
      return this.http.get<GetUserById>(this.UsersApi+ `/${id}`);
  }
  UpdateEmployee(id:any , model:GetUserById){
    return this.http.put<GetUserById>(this.UsersApi+ `/${id}`,model);
  }
  DeleteUser(id:any){
    return this.http.delete(this.UsersApi + `/${id}`);
  }
}
