import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Post } from '../../services/api.service';

@Component({
  selector: 'app-blog-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './blog-form.component.html',
  styleUrls: ['./blog-form.component.css']
})
export class BlogFormComponent implements OnInit {
  post: Post = { id: 0, title: '', content: '', createdAt: new Date().toISOString() };
  isEditMode = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.apiService.getPost(Number(id)).subscribe({
        next: (data: Post) => this.post = data,
        error: (err: any) => this.error = 'Failed to load post'
      });
    }
  }

  onSubmit(): void {
    if (this.isEditMode) {
      this.apiService.updatePost(this.post.id, this.post).subscribe({
        next: () => this.router.navigate(['/posts', this.post.id]),
        error: (err: any) => this.error = 'Failed to update post'
      });
    } else {
      this.apiService.createPost(this.post).subscribe({
        next: (createdPost: Post) => this.router.navigate(['/posts', createdPost.id]),
        error: (err: any) => this.error = 'Failed to create post'
      });
    }
  }
}
