import { Component, OnInit } from '@angular/core';
import { CandidateService } from './candidate.service';
import { ICandidate } from '../shared/models/candidate';
import { environment } from 'src/environments/environment';
import { IPagination } from '../shared/models/pagination';

@Component({
  selector: 'app-candidate',
  templateUrl: './candidate.component.html',
  styleUrls: ['./candidate.component.scss']
})
export class CandidateComponent implements OnInit {
  candidates: ICandidate[];
  canShowCandidates = false;
  pageSize = environment.defaultPageSize;
  pageNumber = 1;
  totalCount = 0;

  constructor(
    private candidateService: CandidateService,
  ) { }

  ngOnInit(): void {
    this.getPagedCandidates();
  }

  onPageChanged(pageNumber: number) {
    if (pageNumber === this.pageSize) { return; }

    this.pageNumber = pageNumber;
    this.getPagedCandidates();
  }

  private getPagedCandidates() {
    this.candidateService.getPagedCandidates(
      this.pageNumber, this.pageSize
    ).subscribe((response) => {
      this.candidates = response.body.items;
      this.totalCount = response.body.count;
      this.canShowCandidates = true;
    });
  }
}
