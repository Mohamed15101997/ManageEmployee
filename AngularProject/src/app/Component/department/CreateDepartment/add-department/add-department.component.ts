import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {  FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CreateDepartment } from '../../../../Interfaces/CreateDepartment';
import { DepartmentService } from '../../../../Services/department.service';

@Component({
  selector: 'app-add-department',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './add-department.component.html',
  styleUrl: './add-department.component.css'
})
export class AddDepartmentComponent {
  constructor(private fb:FormBuilder  , private router:Router,private departmentService:DepartmentService) {}
  // Validation
  CreateForm = new FormGroup({
    name: new FormControl('',[
      Validators.minLength(2),
      Validators.maxLength(150),
      Validators.required
    ])
  });
  get NameValid() {
    return this.CreateForm.controls['name'].valid;
  }
  // Create Department Func
  CreateDepartment(){
    const model: CreateDepartment = {
          name: this.CreateForm.value['name'] as string
        };
        this.departmentService.createDepartment(model).subscribe({
          next:(res:any)=>{
            this.router.navigateByUrl('/departments');
          } , error :(error:any)=>{
            console.log(error);

          }
        });
        console.log(this.CreateForm.value);
  }

}
