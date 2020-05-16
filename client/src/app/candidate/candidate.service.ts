import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ICandidate } from '../shared/models/candidate';
import { IPagination } from '../shared/models/pagination';
import { map } from 'rxjs/operators';

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

  getPagedCandidates(pageNumber: number = 0, pageSize: number = 0) {
    const url = `${this.baseUrl}candidates/paged`;

    let params = new HttpParams();

    if (pageNumber) {
      params = params.append('pageNumber', pageNumber.toString());
    }

    if (pageSize) {
      params = params.append('pageSize', pageSize.toString());
    }

    return this.http.get<IPagination<ICandidate>>(url, {observe: 'response', params});
  }
}
