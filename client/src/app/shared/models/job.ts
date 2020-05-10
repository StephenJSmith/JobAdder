export interface IJob {
  jobId: number;
  name: string;
  company: string;
  skills: string;
  jobSkills: IJobSkill[];
}

export interface IJobSkill {
  jobId: number;
  name: string;
  weighting: number;
}
