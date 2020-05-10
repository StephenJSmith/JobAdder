export interface ICandidate {
  candidateId: number;
  name: string;
  firstName: string;
  lastName: string;
  skillTags: string;
  candidateSkills: ICandidateSkill[];
}

export interface ICandidateSkill {
  candidateId: number;
  name: string;
  weighting: number;
}
