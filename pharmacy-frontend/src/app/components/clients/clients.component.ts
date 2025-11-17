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
    if (this.editingId) {
      this.clientService.update(this.editingId, this.newClient).subscribe(() => {
        this.loadClients();
        this.closeForm();
      });
    } else {
      this.clientService.create(this.newClient).subscribe(() => {
        this.loadClients();
        this.closeForm();
      });
    }
  }

  deleteClient(id: number): void {
    if (confirm('Are you sure you want to delete this client?')) {
      this.clientService.delete(id).subscribe(() => {
        this.loadClients();
      });
    }
  }

  closeForm(): void {
    this.showForm = false;
    this.resetForm();
    this.editingId = null;
  }
}
