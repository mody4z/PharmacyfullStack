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
    this.newEmployee = { ...employee };
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
    if (this.editingId) {
      this.employeeService.update(this.editingId, this.newEmployee).subscribe(() => {
        this.loadEmployees();
        this.closeForm();
      });
    } else {
      this.employeeService.create(this.newEmployee).subscribe(() => {
        this.loadEmployees();
        this.closeForm();
      });
    }
  }

  deleteEmployee(id: number): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.delete(id).subscribe(() => {
        this.loadEmployees();
      });
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.resetForm();
    this.editingId = null;
  }
}
