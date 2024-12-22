import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { DepartmentService } from '../../../Services/department.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { DepartmentDetails } from '../../../Interfaces/DepartmentDetails';

@Component({
  selector: 'app-department-details',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './department-details.component.html',
  styleUrl: './department-details.component.css'
})
export class DepartmentDetailsComponent implements OnInit{
  id: string | null = null;
  department!: DepartmentDetails;
    constructor(
      private route: ActivatedRoute,
      private departmentService: DepartmentService,
      private fb: FormBuilder ,
      private router:Router
    ) { }
  ngOnInit(): void {
    this.GetIdFromLink();
    this.GetDepartment();

  }

  GetIdFromLink(): void {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  GetDepartment(): void {
    if (this.id) {
      this.departmentService.GetDepartmentByID(this.id).subscribe({
        next: (res: any) => {
          this.department = res;
          console.log(this.department);
        },
        error: (error: any) => {
          console.log(error);
        }
      });
    }
  }
  }
