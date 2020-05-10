import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JobComponent } from './job.component';
import { SharedModule } from '../shared/shared.module';
import { JobRoutingModule } from './job-routing.module';
import { JobsListComponent } from './jobs-list/jobs-list.component';



@NgModule({
  declarations: [JobComponent, JobsListComponent],
  imports: [
    CommonModule,
    SharedModule,
    JobRoutingModule,
  ]
})
export class JobModule { }
