import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { IJob } from 'src/app/shared/models/job';
import { ISelectedJob } from 'src/app/shared/models/selected-job';

@Component({
  selector: 'app-jobs-list',
  templateUrl: './jobs-list.component.html',
  styleUrls: ['./jobs-list.component.scss']
})
export class JobsListComponent implements OnChanges {
  @Input() jobs: IJob[];
  @Input() totalCount: number;
  @Input() pageNumber: number;
  @Input() pageSize: number;
  @Output() pageChanged = new EventEmitter<number>();
  @Output() selectJob = new EventEmitter<ISelectedJob>();
  title: string;

  constructor() { }

  ngOnChanges(): void {
    this.setTitle();
  }

  onPageChanged(event: any) {
    this.pageChanged.emit(event.page);
  }

  private setTitle() {
    if (!this.jobs || this.jobs.length === 0) {
      this.title = 'There are NO Open Jobs';
    } else {
      this.title = `Page ${this.pageNumber} of Open Jobs (${this.totalCount}) - Company order`;
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
