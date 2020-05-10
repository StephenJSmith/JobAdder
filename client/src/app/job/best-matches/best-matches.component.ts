import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IMatchedJobCandidate } from 'src/app/shared/models/matched-job-candidate';
import { ISelectedJob } from 'src/app/shared/models/selected-job';

@Component({
  selector: 'app-best-matches',
  templateUrl: './best-matches.component.html',
  styleUrls: ['./best-matches.component.scss']
})
export class BestMatchesComponent implements OnInit {
  @Input() selectedJob: ISelectedJob;
  @Input() bestMatches: IMatchedJobCandidate[];
  @Output() toFullJobsList = new EventEmitter<null>();
  title: string;
  subtitle: string;

  constructor() { }

  ngOnInit(): void {
    this.setTitles();
  }

  onToFullJobsList() {
    this.toFullJobsList.emit();
  }

  getMatchedSkillsWeightings(match: IMatchedJobCandidate) {
    let result = '';
    for (let i = 0; i < match.matchedSkills.length; i++) {
      const skill = match.matchedSkills[i];
      const weighting = skill.jobWeighting * skill.candidateWeighting;
      result += `${skill.name} (J:${skill.jobWeighting} x C:${skill.candidateWeighting} = ${weighting})`;

      if (i < match.matchedSkills.length - 1) {
        result += ' + ';
      }
    }

    return result;
  }

  private setTitles() {
    this.title = `Company: ${this.selectedJob.company}  Job: ${this.selectedJob.name}`;

    if (!this.bestMatches.length) {
      this.subtitle = 'NO Matching Candidates';
    } else {
      this.subtitle = ` Top ${this.bestMatches.length} candidates per weightings`;
    }
  }
}
