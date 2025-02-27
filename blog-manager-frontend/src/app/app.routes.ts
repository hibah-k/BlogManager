import { Routes } from '@angular/router';
import { BlogListComponent } from './components/blog-list/blog-list.component';
import { BlogDetailComponent } from './components/blog-detail/blog-detail.component';
import { BlogFormComponent } from './components/blog-form/blog-form.component';

export const routes: Routes = [
  { path: '', component: BlogListComponent },
  { path: 'posts/:id', component: BlogDetailComponent },
  { path: 'new', component: BlogFormComponent },
  { path: 'edit/:id', component: BlogFormComponent }
];
