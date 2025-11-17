import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InOut, CreateInOutDto } from '../models/inout.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class InOutService {
  private apiUrl = 'http://localhost:5091/api/InOuts';

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

  getAll(): Observable<InOut[]> {
    return this.http.get<InOut[]>(this.apiUrl);
  }

  getById(id: number): Observable<InOut> {
    return this.http.get<InOut>(`${this.apiUrl}/${id}`);
  }

  create(transaction: CreateInOutDto): Observable<InOut> {
    return this.http.post<InOut>(this.apiUrl, transaction, { headers: this.getHeaders() });
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
}
