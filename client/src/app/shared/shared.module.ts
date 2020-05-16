import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagerComponent } from './components/pager/pager.component';
import { FormsModule } from '@angular/forms';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';

@NgModule({
  declarations: [
    PagerComponent,
    PagingHeaderComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule.forRoot(),
  ],
  exports: [
    FormsModule,
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
  ]
})
export class SharedModule { }
