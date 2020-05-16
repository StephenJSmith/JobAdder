import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { ICandidate } from 'src/app/shared/models/candidate';

@Component({
  selector: 'app-candidates-list',
  templateUrl: './candidates-list.component.html',
  styleUrls: ['./candidates-list.component.scss']
})
export class CandidatesListComponent implements OnChanges {
  @Input() candidates: ICandidate[];
  @Input() totalCount: number;
  @Input() pageNumber: number;
  @Input() pageSize: number;
  @Output() pageChanged = new EventEmitter<number>();
  title: string;

  constructor() { }

  ngOnChanges(): void {
    this.setTitle();
  }

  onPageChanged(event: any) {
    this.pageChanged.emit(event.page);
  }

  private setTitle() {
    if (this.candidates.length === 0) {
      this.title = 'There are NO current Candidates';
    } else {
      this.title = `Page ${this.pageNumber} of Candidates (${this.totalCount}) - Last Name order`;
    }
  }
}
