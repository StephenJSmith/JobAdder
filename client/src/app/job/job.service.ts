import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IJob } from '../shared/models/job';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getJobs() {
    const url = `${this.baseUrl}jobs`;

    return this.http.get<IJob[]>(url);
  }
}
