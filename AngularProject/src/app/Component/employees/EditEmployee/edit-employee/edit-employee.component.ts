import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CreateUserService } from '../../../../Services/user.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GetUserById } from '../../../../Interfaces/GetUserById';
import { DepartmentService } from '../../../../Services/department.service';
import { Department } from '../../../../Interfaces/Department';

@Component({
  selector: 'app-edit-employee',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './edit-employee.component.html',
  styleUrl: './edit-employee.component.css'
})
export class EditEmployeeComponent implements OnInit{

  id: string | null = null;
  user!: GetUserById;
  Departments!:Department[];


    constructor(
      private route: ActivatedRoute,
      private userService: CreateUserService,
      private fb: FormBuilder ,
      private router:Router ,
      private departmentService:DepartmentService

    ) { }

  ngOnInit(): void {
    this.GetIdFromLink();
    if (this.id) {
      this.GetEmployee();
    }
    this.LoadDepartments();
  }
// Validation
  EditForm = new FormGroup({
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
    ]), confirmPassword: new FormControl('', Validators.required),
    departmentId: new FormControl(1, Validators.required)}
  );
  get NameValid() {
    return this.EditForm.controls['name'].valid;
  }
  get UserNameValid() {
    return this.EditForm.controls['userName'].valid;
  }
  get AddressValid() {
    return this.EditForm.controls['address'].valid;
  }
  get SalaryValid() {
    return this.EditForm.controls['salary'].valid;
  }
  get PasswordValid() {
    return this.EditForm.controls['password'].valid;
  }

  GetIdFromLink(): void {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  GetEmployee(): void {
    if (this.id) {
      this.userService.GetEmployeeByID(this.id).subscribe({
        next: (res: any) => {
          this.user = res;
          console.log(this.user);
          this.EditForm.patchValue({
            name: this.user.name,
            userName: this.user.userName,
            address: this.user.address,
            salary: this.user.salary,
            departmentId: this.user.departmentId
          });
        },
        error: (error: any) => {
          console.log(error);
        }
      });
    }
  }

  UpdateEmployee(){
    const model: GetUserById = {
              id : this.user.id ,
              name: this.EditForm.value['name'] as string ,
              userName : this.EditForm.value['userName'] as string ,
              address : this.EditForm.value['address'] as string ,
              salary : this.EditForm.value['salary'] as number ,
              departmentId : this.EditForm.value['departmentId'] as number ,
            };
            this.userService.UpdateEmployee(this.id,model).subscribe({
              next:(res:any)=>{
                this.router.navigateByUrl('/employees');
              } , error :(error:any)=>{
                console.log(error);
              }
            });
            console.log(this.EditForm.value);
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

}
