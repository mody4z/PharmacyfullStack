import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ClientService } from '../../services/client.service';
import { AuthService } from '../../services/auth.service';
import { Client, CreateClientDto } from '../../models/client.model';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {
  clients: Client[] = [];
  filteredClients: Client[] = [];
  searchTerm = '';
  showForm = false;
  editingId: number | null = null;
  isLoggedIn = false;
  loading = false;
  deletingId: number | null = null;

  newClient: CreateClientDto = {
    name: '',
    phoneNumber: '',
    email: '',
    address: ''
  };

  constructor(
    private clientService: ClientService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.loadClients();
  }

  loadClients(): void {
    this.clientService.getAll().subscribe(data => {
      this.clients = data;
      this.filteredClients = data;
    });
  }

  filterClients(): void {
    if (!this.searchTerm) {
      this.filteredClients = this.clients;
    } else {
      this.filteredClients = this.clients.filter(c =>
        c.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        c.email.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  }

  openForm(): void {
    this.showForm = true;
    this.editingId = null;
    this.resetForm();
  }

  editClient(client: Client): void {
    this.showForm = true;
    this.editingId = client.id!;
    this.newClient = { ...client };
  }

  resetForm(): void {
    this.newClient = {
      name: '',
      phoneNumber: '',
      email: '',
      address: ''
    };
  }

  saveClient(): void {
    this.loading = true;
    if (this.editingId) {
      this.clientService.update(this.editingId, this.newClient).subscribe({
        next: () => {
          this.loadClients();
          this.closeForm();
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
    } else {
      this.clientService.create(this.newClient).subscribe({
        next: () => {
          this.loadClients();
          this.closeForm();
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
    }
  }

  deleteClient(id: number): void {
    if (this.deletingId) return;
    if (confirm('Are you sure you want to delete this client?')) {
      this.deletingId = id;
      this.clientService.delete(id).subscribe({
        next: () => {
          this.loadClients();
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
