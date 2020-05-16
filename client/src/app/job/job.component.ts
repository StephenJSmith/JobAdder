import { Component, OnInit } from '@angular/core';
import { IJob } from '../shared/models/job';
import { JobService } from './job.service';
import { ISelectedJob } from '../shared/models/selected-job';
import { IMatchedJobCandidate } from '../shared/models/matched-job-candidate';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit {
  jobs: IJob[];
  matchedJobCandidates: IMatchedJobCandidate[];
  selectedJob: ISelectedJob;
  canShowJobs = false;
  canShowBestMatches = false;
  pageSize = environment.defaultPageSize;
  pageNumber = 1;
  totalCount = 0;

  constructor(
    private jobService: JobService,
  ) { }

  ngOnInit(): void {
    this.getPagedJobs();
  }

  onSelectedJob(selectedJob: ISelectedJob) {
    this.selectedJob = selectedJob;
    this.getMatchedJobCandidates(selectedJob);
  }

  onToFullJobsList() {
    this.canShowBestMatches = false;
    this.canShowJobs = true;
  }

  onPageChanged(pageNumber: number) {
    if (pageNumber === this.pageSize) { return; }

    this.pageNumber = pageNumber;
    this.getPagedJobs();
  }

  private getPagedJobs() {
    this.jobService.getPagedJobs(
      this.pageNumber, this.pageSize
    ).subscribe((response) => {
      this.jobs = response.body.items;
      this.totalCount = response.body.count;
      this.canShowJobs = true;
      this.canShowBestMatches = false;
    }, error => {
      console.log(error);
    });
  }

  private getMatchedJobCandidates(selectedJob: ISelectedJob) {
    this.jobService.getMatchedJobCandidates(selectedJob)
      .subscribe((matchedCandidates: IMatchedJobCandidate[]) => {
        this.matchedJobCandidates = matchedCandidates;
        this.canShowBestMatches = true;
        this.canShowJobs = false;
      });
  }
}
