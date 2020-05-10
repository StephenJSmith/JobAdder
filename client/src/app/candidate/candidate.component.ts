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
  title: string;

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
        this.setTitle();
      }, error => {
        console.log(error);
      });
  }

  private setTitle() {
    if (this.candidates.length === 0) {
      this.title = 'There are NO current Candidates';
    } else {
      this.title = `Full list of Candidates (${this.candidates.length}) - Last Name order`;
    }
  }
}
