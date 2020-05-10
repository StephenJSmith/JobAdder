import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'candidates',
    loadChildren: () => import('./candidate/candidate.module')
      .then(mod => mod.CandidateModule)
  },
  {
    path: 'jobs',
    loadChildren: () => import('./job/job.module')
      .then(mod => mod.JobModule)
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
