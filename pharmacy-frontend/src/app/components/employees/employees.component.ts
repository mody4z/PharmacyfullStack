import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { AuthService } from '../../services/auth.service';
import { Employee, CreateEmployeeDto } from '../../models/employee.model';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  employees: Employee[] = [];
  filteredEmployees: Employee[] = [];
  searchTerm = '';
  showForm = false;
  editingId: number | null = null;
  isLoggedIn = false;
  loading = false;
  deletingId: number | null = null;

  newEmployee: CreateEmployeeDto = {
    name: '',
    position: '',
    phoneNumber: '',
    email: '',
    salary: 0,
    hireDate: ''
  };

  constructor(
    private employeeService: EmployeeService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getAll().subscribe(data => {
      console.log('Loaded employees:', data);
      this.employees = data;
      this.filteredEmployees = data;
    });
  }

  filterEmployees(): void {
    if (!this.searchTerm) {
      this.filteredEmployees = this.employees;
    } else {
      this.filteredEmployees = this.employees.filter(e =>
        e.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        e.position.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  }

  openForm(): void {
    this.showForm = true;
    this.editingId = null;
    this.resetForm();
  }

  editEmployee(employee: Employee): void {
    this.showForm = true;
    this.editingId = employee.id!;
    // Format date for input field
    const hireDate = employee.hireDate ? new Date(employee.hireDate).toISOString().split('T')[0] : '';
    this.newEmployee = { 
      name: employee.name,
      position: employee.position,
      phoneNumber: employee.phoneNumber,
      email: employee.email,
      salary: employee.salary,
      hireDate: hireDate
    };
    console.log('Editing employee:', this.newEmployee);
  }

  resetForm(): void {
    this.newEmployee = {
      name: '',
      position: '',
      phoneNumber: '',
      email: '',
      salary: 0,
      hireDate: ''
    };
  }

  saveEmployee(): void {
    this.loading = true;
    // Format date to ISO string if needed
    const employeeData = {
      ...this.newEmployee,
      hireDate: this.newEmployee.hireDate ? new Date(this.newEmployee.hireDate).toISOString() : new Date().toISOString()
    };

    if (this.editingId) {
      this.employeeService.update(this.editingId, employeeData).subscribe({
        next: () => {
          this.loadEmployees();
          this.closeForm();
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
    } else {
      this.employeeService.create(employeeData).subscribe({
        next: () => {
          this.loadEmployees();
          this.closeForm();
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
    }
  }

  deleteEmployee(id: number): void {
    if (this.deletingId) return;
    if (confirm('Are you sure you want to delete this employee?')) {
      this.deletingId = id;
      this.employeeService.delete(id).subscribe({
        next: () => {
          this.loadEmployees();
          this.deletingId = null;
        },
        error: () => {
          this.deletingId = null;
        }
      });
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.resetForm();
    this.editingId = null;
  }
}
