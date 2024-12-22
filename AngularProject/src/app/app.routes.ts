import { Routes } from '@angular/router';
import { LoginComponent } from './Component/login/login.component';
import { LayoutComponent } from './Component/layout/layout.component';
import { DashboardComponent } from './Component/dashboard/dashboard.component';
import { RegisterComponent } from './Component/register/register.component';
import { DepartmentComponent } from './Component/department/department.component';
import { EmployeesComponent } from './Component/employees/employees.component';
import { AddDepartmentComponent } from './Component/department/CreateDepartment/add-department/add-department.component';
import { EditDepartmentComponent } from './Component/department/EditDepartment/edit-department/edit-department.component';
import { DepartmentDetailsComponent } from './Component/department/department-details/department-details.component';
import { EditEmployeeComponent } from './Component/employees/EditEmployee/edit-employee/edit-employee.component';
import { EmployeeDetailsComponent } from './Component/employees/employeeDetails/employee-details/employee-details.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      {
        path: 'departments',
        component: DepartmentComponent,
      },
      {
        path: 'employees',
        component: EmployeesComponent,
      },
      {
        path: 'CreateDepartment',
        component: AddDepartmentComponent,
      },
      {
        path: 'EditDepartment/:id',
        component: EditDepartmentComponent
      },
      {
        path: 'department/:id',
        component: DepartmentDetailsComponent,
      },{
        path: 'EditEmployee/:id',
        component: EditEmployeeComponent
      },{
        path: 'employee/:id',
        component: EmployeeDetailsComponent,
      },
    ],
  },
];
