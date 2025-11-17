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
    if (this.editingId) {
      this.medicineService.update(this.editingId, this.newMedicine).subscribe(() => {
        this.loadMedicines();
        this.closeForm();
      });
    } else {
      this.medicineService.create(this.newMedicine).subscribe(() => {
        this.loadMedicines();
        this.closeForm();
      });
    }
  }

  deleteMedicine(id: number): void {
    if (confirm('Are you sure you want to delete this medicine?')) {
      this.medicineService.delete(id).subscribe(() => {
        this.loadMedicines();
      });
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.resetForm();
    this.editingId = null;
  }
}
