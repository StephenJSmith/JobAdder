import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CandidateComponent } from './candidate.component';
import { SharedModule } from '../shared/shared.module';
import { CandidateRoutingModule } from './candidate-routing.module';



@NgModule({
  declarations: [CandidateComponent],
  imports: [
    CommonModule,
    SharedModule,
    CandidateRoutingModule,
  ]
})
export class CandidateModule { }
