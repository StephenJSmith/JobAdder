import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IJob } from '../shared/models/job';
import { IMatchedJobCandidate } from '../shared/models/matched-job-candidate';
import { ISelectedJob } from '../shared/models/selected-job';
import { IPagination } from '../shared/models/pagination';

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

  getPagedJobs(pageNumber: number = 0, pageSize: number = 0) {
    const url = `${this.baseUrl}jobs/paged`;

    let params = new HttpParams();

    if (pageNumber) {
      params = params.append('pageNumber', pageNumber.toString());
    }

    if (pageSize) {
      params = params.append('pageSize', pageSize.toString());
    }

    return this.http.get<IPagination<IJob>>(url, {observe: 'response', params});
  }

  getMatchedJobCandidates(job: ISelectedJob) {
    const url = `${this.baseUrl}jobs/${job.jobId}/candidates/${job.topNumber}`;

    return this.http.get<IMatchedJobCandidate[]>(url);
  }
}
