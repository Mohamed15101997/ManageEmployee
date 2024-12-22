import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { DepartmentService } from '../../../../Services/department.service';
import { Department } from '../../../../Interfaces/Department';

@Component({
  selector: 'app-edit-department',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './edit-department.component.html',
  styleUrls: ['./edit-department.component.css']
})
export class EditDepartmentComponent implements OnInit {
  id: string | null = null;
  department!: Department;

  constructor(
    private route: ActivatedRoute,
    private departmentService: DepartmentService,
    private fb: FormBuilder ,
    private router:Router
  ) { }

  ngOnInit(): void {
    this.GetIdFromLink();
    this.initForm();
    if (this.id) {
      this.GetDepartment();
    }
  }
  // Validation
  EditForm = new FormGroup({
    name: new FormControl('',[
      Validators.minLength(2),
      Validators.maxLength(150),
      Validators.required
    ])
  });
  get NameValid() {
    return this.EditForm.controls['name'].valid;
  }

  GetIdFromLink(): void {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  initForm(): void {
    this.EditForm = this.fb.group({
      name: [''],
    });
  }

  GetDepartment(): void {
    if (this.id) {
      this.departmentService.GetDepartmentByID(this.id).subscribe({
        next: (res: any) => {
          this.department = res;
          console.log(this.department);
          this.EditForm.patchValue({
            name: this.department.name,
          });
        },
        error: (error: any) => {
          console.log(error);
        }
      });
    }
  }
  UpdateDepartment(){
    const model: Department = {
          id : this.department.id ,
          name: this.EditForm.value['name'] as string
        };
        this.departmentService.UpdateDepartment(this.id,model).subscribe({
          next:(res:any)=>{
            this.router.navigateByUrl('/departments');
          } , error :(error:any)=>{
            console.log(error);
          }
        });
        console.log(this.EditForm.value);
  }
}
