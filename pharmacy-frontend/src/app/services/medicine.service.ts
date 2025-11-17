import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Medicine, CreateMedicineDto, UpdateMedicineDto } from '../models/medicine.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {
  private apiUrl = 'https://pharmacy-mahmoud-shehata.runasp.net/api/Medicines';

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': token ? `Bearer ${token}` : ''
    });
  }

  getAll(): Observable<Medicine[]> {
    return this.http.get<Medicine[]>(this.apiUrl);
  }

  getById(id: number): Observable<Medicine> {
    return this.http.get<Medicine>(`${this.apiUrl}/${id}`);
  }

  create(medicine: CreateMedicineDto): Observable<Medicine> {
    return this.http.post<Medicine>(this.apiUrl, medicine, { headers: this.getHeaders() });
  }

  update(id: number, medicine: UpdateMedicineDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, medicine, { headers: this.getHeaders() });
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
}
