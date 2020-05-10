import { Component, OnInit } from '@angular/core';
import { IJob } from '../shared/models/job';
import { JobService } from './job.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit {
  jobs: IJob[];
  title: string;

  constructor(
    private jobService: JobService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.getJobs();
  }

  private getJobs() {
    this.jobService.getJobs()
      .subscribe((jobs: IJob[]) => {
        this.jobs = jobs;
        this.setTitle();
      }, error => {
        console.log(error);
      });
  }

  private setTitle() {
    if (this.jobs.length === 0) {
      this.title = 'There are NO current Jobs';
    } else {
      this.title = `Full list of Jobs (${this.jobs.length}) - Company order`;
    }
  }

  onTopCandidates(jobId: number, topNumber: number) {
    console.log(`Job: ${jobId} - Top ${topNumber}`);
  }
}
