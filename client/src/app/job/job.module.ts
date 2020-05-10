import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JobComponent } from './job.component';
import { SharedModule } from '../shared/shared.module';
import { JobRoutingModule } from './job-routing.module';
import { JobsListComponent } from './jobs-list/jobs-list.component';
import { BestMatchesComponent } from './best-matches/best-matches.component';



@NgModule({
  declarations: [JobComponent, JobsListComponent, BestMatchesComponent],
  imports: [
    CommonModule,
    SharedModule,
    JobRoutingModule,
  ]
})
export class JobModule { }
