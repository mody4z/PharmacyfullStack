import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { InOutService } from '../../services/inout.service';
import { MedicineService } from '../../services/medicine.service';
import { EmployeeService } from '../../services/employee.service';
import { ClientService } from '../../services/client.service';
import { AuthService } from '../../services/auth.service';
import { InOut, CreateInOutDto } from '../../models/inout.model';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit {
  transactions: InOut[] = [];
  medicines: any[] = [];
  employees: any[] = [];
  clients: any[] = [];
  showForm = false;
  isLoggedIn = false;

  newTransaction: CreateInOutDto = {
    medicineId: 0,
    employeeId: 0,
    clientId: undefined,
    transactionType: 'In',
    quantity: 0,
    notes: ''
  };

  constructor(
    private inOutService: InOutService,
    private medicineService: MedicineService,
    private employeeService: EmployeeService,
    private clientService: ClientService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.loadTransactions();
    this.loadDropdownData();
  }

  loadTransactions(): void {
    this.inOutService.getAll().subscribe(data => {
      this.transactions = data;
    });
  }

  loadDropdownData(): void {
    this.medicineService.getAll().subscribe(data => this.medicines = data);
    this.employeeService.getAll().subscribe(data => this.employees = data);
    this.clientService.getAll().subscribe(data => this.clients = data);
  }

  openForm(): void {
    this.showForm = true;
    this.resetForm();
  }

  resetForm(): void {
    const currentUser = this.authService.getCurrentUser();
    this.newTransaction = {
      medicineId: 0,
      employeeId: currentUser?.employeeId || 0,
      clientId: undefined,
      transactionType: 'In',
      quantity: 0,
      notes: ''
    };
  }

  saveTransaction(): void {
    this.inOutService.create(this.newTransaction).subscribe(() => {
      this.loadTransactions();
      this.closeForm();
    });
  }

  deleteTransaction(id: number): void {
    if (confirm('Are you sure you want to delete this transaction?')) {
      this.inOutService.delete(id).subscribe(() => {
        this.loadTransactions();
      });
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.resetForm();
  }
}
