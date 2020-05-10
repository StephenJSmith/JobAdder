import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IJob } from 'src/app/shared/models/job';
import { ISelectedJob } from 'src/app/shared/models/selected-job';

@Component({
  selector: 'app-jobs-list',
  templateUrl: './jobs-list.component.html',
  styleUrls: ['./jobs-list.component.scss']
})
export class JobsListComponent implements OnInit {
  @Input() jobs: IJob[];
  @Output() selectJob = new EventEmitter<ISelectedJob>();
  title: string;

  constructor() { }

  ngOnInit(): void {
    this.setTitle();
  }

  private setTitle() {
    if (!this.jobs || this.jobs.length === 0) {
      this.title = 'There are NO Open Jobs';
    } else {
      this.title = `Full list of Open Jobs (${this.jobs.length}) - Company order`;
    }
  }

  onTopCandidates(job: IJob, topNumber: number) {
    const selectedJob: ISelectedJob = {
      jobId: job.jobId,
      company: job.company,
      name: job.name,
      topNumber
    };
    this.selectJob.emit(selectedJob);
  }
}
