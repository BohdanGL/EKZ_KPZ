import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Visit } from './visit.model';

@Injectable()
export class VisitsService {
  private apiUrl = environment.apiUrl + "/Visits";

  constructor(private httpClient: HttpClient) {}

  public getVisits(): Observable<Visit[]> {
    return this.httpClient.get<Visit[]>(this.apiUrl);
  }

  public editVisit(updatedVisit: Visit): Observable<number> {
    return this.httpClient.put<number>(this.apiUrl, updatedVisit);
  }

  public createVisit(newVisit: Visit): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl, newVisit);
  }

  public deleteVisit(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`);
  }
}