import { Component, OnInit } from '@angular/core';
import { CandidateService } from './candidate.service';
import { ICandidate } from '../shared/models/candidate';

@Component({
  selector: 'app-candidate',
  templateUrl: './candidate.component.html',
  styleUrls: ['./candidate.component.scss']
})
export class CandidateComponent implements OnInit {
  candidates: ICandidate[];
  canShowCandidates = false;

  constructor(
    private candidateService: CandidateService,
  ) { }

  ngOnInit(): void {
    this.getCandidates();
  }

  private getCandidates() {
    this.candidateService.getCandidates()
      .subscribe((candidates: ICandidate[]) => {
        this.candidates = candidates;
        this.canShowCandidates = true;
      }, error => {
        console.log(error);
      });
  }
}
