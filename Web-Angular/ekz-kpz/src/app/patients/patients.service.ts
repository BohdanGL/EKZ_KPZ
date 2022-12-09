import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Patient } from './patient.model';

@Injectable()
export class PatientsService {
  private apiUrl = environment.apiUrl + "/Patients";

  constructor(private httpClient: HttpClient) {}

  public getPatients(): Observable<Patient[]> {
    return this.httpClient.get<Patient[]>(this.apiUrl);
  }

  public editPatient(updatedPatient: Patient): Observable<number> {
    return this.httpClient.put<number>(this.apiUrl, updatedPatient);
  }

  public createPatient(newPatient: Patient): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl, newPatient);
  }

  public deletePatient(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`);
  }
}

  