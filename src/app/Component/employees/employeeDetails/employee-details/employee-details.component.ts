import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CreateUserService } from '../../../../Services/user.service';
import { GetUser } from '../../../../Interfaces/GetUsers';

@Component({
  selector: 'app-employee-details',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.css'
})
export class EmployeeDetailsComponent implements OnInit{
    id: string | null = null;
    user!: GetUser;
      constructor(
        private route: ActivatedRoute,
        private fb: FormBuilder ,
        private router:Router ,
        private userService:CreateUserService
      ) { }
  ngOnInit(): void {
    this.GetIdFromLink();
    this.GetUser();

  }
  GetIdFromLink(): void {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  GetUser(): void {
    if (this.id) {
      this.userService.GetEmployeeByID(this.id).subscribe({
        next: (res: any) => {
          this.user = res;
          console.log(this.user);
        },
        error: (error: any) => {
          console.log(error);
        }
      });
    }
  }
}
