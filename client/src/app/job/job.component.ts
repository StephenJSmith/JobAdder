import { Component, OnInit } from '@angular/core';
import { IJob } from '../shared/models/job';
import { JobService } from './job.service';
import { ISelectedJob } from '../shared/models/selected-job';
import { IMatchedJobCandidate } from '../shared/models/matched-job-candidate';

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

  constructor(
    private jobService: JobService,
  ) { }

  ngOnInit(): void {
    this.getJobs();
  }

  onSelectedJob(selectedJob: ISelectedJob) {
    this.selectedJob = selectedJob;
    this.getMatchedJobCandidates(selectedJob);
  }

  onToFullJobsList() {
    this.canShowBestMatches = false;
    this.canShowJobs = true;
  }

  private getJobs() {
    this.jobService.getJobs()
      .subscribe((jobs: IJob[]) => {
        this.jobs = jobs;
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
