import { Component, OnInit } from '@angular/core';
import { IJob } from '../shared/models/job';
import { JobService } from './job.service';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit {
  jobs: IJob[];

  constructor(
    private jobService: JobService,
  ) { }

  ngOnInit(): void {
    this.getJobs();
  }

  private getJobs() {
    this.jobService.getJobs()
      .subscribe((jobs: IJob[]) => {
        this.jobs = jobs;
        console.log(this.jobs);
      }, error => {
        console.log(error);
      });
  }
}
