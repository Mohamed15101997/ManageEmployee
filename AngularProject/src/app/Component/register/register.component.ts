import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {  AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, ValidationErrors, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CreateUserService } from '../../Services/user.service';
import { User } from '../../Interfaces/User';
import { DepartmentService } from '../../Services/department.service';
import { Department } from '../../Interfaces/Department';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
    Departments!:Department[];
  constructor(private fb:FormBuilder , private createUserService:CreateUserService , private router:Router , private departmentService:DepartmentService) {}

  ngOnInit(): void {
  this.LoadDepartments();
  }
  // Validation
  RegisterForm = new FormGroup({
    name: new FormControl('',[
      Validators.minLength(3),
      Validators.maxLength(150),
      Validators.required
    ]),
    userName: new FormControl('',Validators.required) ,
    address: new FormControl('',[
      Validators.minLength(3),
      Validators.maxLength(200),
      Validators.required
    ]),salary: new FormControl(1,[
      Validators.required,
      Validators.min(1),
      Validators.max(50000)
    ]),password: new FormControl('',[
      Validators.required,
      Validators.minLength(5)
    ]), confirmPassword: new FormControl('', Validators.required

    ),departmentId: new FormControl(1, Validators.required)},
);
  get NameValid() {
    return this.RegisterForm.controls['name'].valid;
  }
  get UserNameValid() {
    return this.RegisterForm.controls['userName'].valid;
  }
  get AddressValid() {
    return this.RegisterForm.controls['address'].valid;
  }
  get SalaryValid() {
    return this.RegisterForm.controls['salary'].valid;
  }
  get PasswordValid() {
    return this.RegisterForm.controls['password'].valid;
  }

  // Load Departments
  LoadDepartments(){
    this.departmentService.GetDepartments().subscribe({
      next:(res:any)=>{
        console.log(res);
        this.Departments = res;
      } , error :(error:any)=>{
        console.log(error);
      }
    });
  }

  // Create Account Func
  CreateAccount(){
    const model: User = {
      name: this.RegisterForm.value['name'] as string,
      userName: this.RegisterForm.value['userName'] as string,
      address: this.RegisterForm.value['address'] as string,
      salary: this.RegisterForm.value['salary'] as number,
      departmentId: this.RegisterForm.value['departmentId'] as number,
      password: this.RegisterForm.value['password'] as string,
      confirmPassword: this.RegisterForm.value['confirmPassword'] as string,
    };
    this.createUserService.createAccount(model).subscribe({
      next:(res:any)=>{
        this.router.navigateByUrl('');
      } , error :(error:any)=>{
        console.log(error);

      }
    });
    console.log(this.RegisterForm.value);
  }
}
