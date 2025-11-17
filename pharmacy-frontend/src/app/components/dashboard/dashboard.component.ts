import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MedicineService } from '../../services/medicine.service';
import { EmployeeService } from '../../services/employee.service';
import { ClientService } from '../../services/client.service';
import { InOutService } from '../../services/inout.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  currentUser: any;
  stats = {
    medicines: 0,
    employees: 0,
    clients: 0,
    transactions: 0
  };

  constructor(
    private authService: AuthService,
    private medicineService: MedicineService,
    private employeeService: EmployeeService,
    private clientService: ClientService,
    private inOutService: InOutService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    this.loadStats();
  }

  loadStats(): void {
    this.medicineService.getAll().subscribe(data => this.stats.medicines = data.length);
    this.employeeService.getAll().subscribe(data => this.stats.employees = data.length);
    this.clientService.getAll().subscribe(data => this.stats.clients = data.length);
    this.inOutService.getAll().subscribe(data => this.stats.transactions = data.length);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
