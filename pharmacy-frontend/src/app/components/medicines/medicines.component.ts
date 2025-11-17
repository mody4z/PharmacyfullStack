import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MedicineService } from '../../services/medicine.service';
import { AuthService } from '../../services/auth.service';
import { Medicine, CreateMedicineDto } from '../../models/medicine.model';

@Component({
  selector: 'app-medicines',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './medicines.component.html',
  styleUrls: ['./medicines.component.css']
})
export class MedicinesComponent implements OnInit {
  medicines: Medicine[] = [];
  filteredMedicines: Medicine[] = [];
  searchTerm = '';
  showForm = false;
  editingId: number | null = null;
  isLoggedIn = false;
  loading = false;
  deletingId: number | null = null;

  newMedicine: CreateMedicineDto = {
    name: '',
    category: '',
    manufacturer: '',
    description: '',
    price: 0,
    stockQuantity: 0,
    expiryDate: ''
  };

  constructor(
    private medicineService: MedicineService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.loadMedicines();
  }

  loadMedicines(): void {
    this.medicineService.getAll().subscribe(data => {
      this.medicines = data;
      this.filteredMedicines = data;
    });
  }

  filterMedicines(): void {
    if (!this.searchTerm) {
      this.filteredMedicines = this.medicines;
    } else {
      this.filteredMedicines = this.medicines.filter(m =>
        m.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        m.category.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  }

  openForm(): void {
    this.showForm = true;
    this.editingId = null;
    this.resetForm();
  }

  editMedicine(medicine: Medicine): void {
    this.showForm = true;
    this.editingId = medicine.id!;
    this.newMedicine = { ...medicine };
  }

  resetForm(): void {
    this.newMedicine = {
      name: '',
      category: '',
      manufacturer: '',
      description: '',
      price: 0,
      stockQuantity: 0,
      expiryDate: ''
    };
  }

  saveMedicine(): void {
    this.loading = true;
    if (this.editingId) {
      this.medicineService.update(this.editingId, this.newMedicine).subscribe({
        next: () => {
          this.loadMedicines();
          this.closeForm();
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
    } else {
      this.medicineService.create(this.newMedicine).subscribe({
        next: () => {
          this.loadMedicines();
          this.closeForm();
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
    }
  }

  deleteMedicine(id: number): void {
    if (this.deletingId) return;
    if (confirm('Are you sure you want to delete this medicine?')) {
      this.deletingId = id;
      this.medicineService.delete(id).subscribe({
        next: () => {
          this.loadMedicines();
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
