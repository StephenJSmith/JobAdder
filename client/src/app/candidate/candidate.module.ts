import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CandidateComponent } from './candidate.component';
import { SharedModule } from '../shared/shared.module';
import { CandidateRoutingModule } from './candidate-routing.module';
import { CandidatesListComponent } from './candidates-list/candidates-list.component';



@NgModule({
  declarations: [CandidateComponent, CandidatesListComponent],
  imports: [
    CommonModule,
    SharedModule,
    CandidateRoutingModule,
  ]
})
export class CandidateModule { }
