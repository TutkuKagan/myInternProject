import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../core/services/TaskService';
import { ITaskComment } from '../../shared/models/task.model';

@Component({
  selector: 'app-task-comments',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-comments.html',
  styleUrls: ['./task-comments.scss']
})
export class TaskCommentsComponent implements OnInit {
  @Input({ required: true }) taskId!: string;
  @Input() currentUserId?: string;

  comments: ITaskComment[] = [];
  newCommentText: string = '';
  isLoading: boolean = false;
  isSubmitting: boolean = false;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    if (this.taskId) {
      this.loadComments();
    }
  }

  loadComments(): void {
    this.isLoading = true;
    this.taskService.getCommentsByTaskId(this.taskId).subscribe({
      next: (data) => {
        this.comments = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Comments loading error:', err);
        this.isLoading = false;
      }
    });
  }

  submitComment(): void {
    if (!this.newCommentText.trim() || this.isSubmitting) return;

    this.isSubmitting = true;
    this.taskService.addComment({ taskId: this.taskId, comment: this.newCommentText.trim() }).subscribe({
      next: (createdComment) => {
        this.comments.unshift(createdComment);
        this.newCommentText = '';
        this.isSubmitting = false;
      },
      error: (err) => {
        console.error('Comment adding error:', err);
        this.isSubmitting = false;
      }
    });
  }

  deleteComment(commentId: string): void {
    if (!confirm('Are you sure you want to delete this comment?')) return;

    this.taskService.deleteComment(commentId).subscribe({
      next: () => {
        this.comments = this.comments.filter(c => c.id !== commentId);
      },
      error: (err) => {
        console.error('Comment deletion error:', err);
      }
    });
  }
}