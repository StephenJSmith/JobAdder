import { Component, OnInit, Input } from '@angular/core';
import { ICandidate } from 'src/app/shared/models/candidate';

@Component({
  selector: 'app-candidates-list',
  templateUrl: './candidates-list.component.html',
  styleUrls: ['./candidates-list.component.scss']
})
export class CandidatesListComponent implements OnInit {
  @Input() candidates: ICandidate[];
  title: string;

  constructor() { }

  ngOnInit(): void {
    this.setTitle();
  }

  private setTitle() {
    if (this.candidates.length === 0) {
      this.title = 'There are NO current Candidates';
    } else {
      this.title = `Full list of Candidates (${this.candidates.length}) - Last Name order`;
    }
  }
}
