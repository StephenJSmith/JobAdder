import { Component, OnInit } from '@angular/core';
import { IJob } from '../shared/models/job';
import { JobService } from './job.service';
import { Router } from '@angular/router';
import { ISelectedJob } from '../shared/models/selected-job';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit {
  jobs: IJob[];
  canShowJobs = false;

  constructor(
    private jobService: JobService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.getJobs();
  }

  onSelectedJob(selectedJob: ISelectedJob) {
    console.log(selectedJob);
  }

  private getJobs() {
    this.jobService.getJobs()
      .subscribe((jobs: IJob[]) => {
        this.jobs = jobs;
        this.canShowJobs = true;
      }, error => {
        console.log(error);
      });
  }
}
