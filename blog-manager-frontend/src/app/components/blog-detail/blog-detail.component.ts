import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApiService, Post } from '../../services/api.service';

@Component({
  selector: 'app-blog-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  post?: Post;
  loading = true;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.apiService.getPost(id).subscribe({
      next: (data: Post) => {
        this.post = data;
        this.loading = false;
      },
      error: (err: any) => {
        this.error = 'Failed to load post';
        this.loading = false;
      }
    });
  }

  editPost(): void {
    if (this.post) {
      this.router.navigate(['/edit', this.post.id]);
    }
  }

  deletePost(): void {
    if (this.post && confirm('Are you sure you want to delete this post?')) {
      this.apiService.deletePost(this.post.id).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/']);
  }
}
