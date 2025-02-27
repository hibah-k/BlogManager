import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';  
import { ApiService, Post } from '../../services/api.service';

@Component({
  selector: 'app-blog-list',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent implements OnInit {
  posts: Post[] = [];
  loading = true;
  error = '';
  searchTerm = '';

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts(): void {
    this.apiService.getPosts().subscribe({
      next: (data: Post[]) => {
        this.posts = data;
        this.loading = false;
      },
      error: (err: any) => {
        this.error = 'Failed to load posts';
        this.loading = false;
      }
    });
  }


  get filteredPosts(): Post[] {
    if (!this.searchTerm) {
      return this.posts;
    }
    return this.posts.filter(post =>
      post.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      post.content.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  viewPost(id: number): void {
    this.router.navigate(['/posts', id]);
  }

  editPost(id: number): void {
    this.router.navigate(['/edit', id]);
  }

  deletePost(id: number): void {
    if (confirm('Are you sure you want to delete this post?')) {
      this.apiService.deletePost(id).subscribe({
        next: () => this.loadPosts(), 
        error: (err: any) => {
          this.error = 'Failed to delete post';
        }
      });
    }
  }
}
