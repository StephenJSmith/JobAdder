import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ICandidate } from '../shared/models/candidate';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCandidates() {
    const url = `${this.baseUrl}candidates`;

    return this.http.get<ICandidate[]>(url);
  }
}
