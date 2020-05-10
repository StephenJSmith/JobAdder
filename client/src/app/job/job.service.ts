import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IJob } from '../shared/models/job';
import { IMatchedJobCandidate } from '../shared/models/matched-job-candidate';
import { ISelectedJob } from '../shared/models/selected-job';

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

  getMatchedJobCandidates(job: ISelectedJob) {
    const url = `${this.baseUrl}jobs/${job.jobId}/candidates/${job.topNumber}`;

    return this.http.get<IMatchedJobCandidate[]>(url);
  }
}
