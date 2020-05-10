export interface IMatchedJobCandidate {
  jobId: number;
  candidateId: number;
  firstName: string;
  lastName: string;
  weightingsSum: number;
  skillsCount: number;
  matchedSkills: IMatchedSkill[];
}

export interface IMatchedSkill {
  jobId: number;
  candidateId: number;
  name: string;
  jobWeighting: number;
  candidateWeighting: number;
}
