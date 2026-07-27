import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TaskCard } from '../task-card/task-card';
import { TaskService } from '../../../core/services/TaskService';
import { NotificationService } from '../../../core/services/NotificationService';
import { ITask } from '../../../shared/models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, RouterModule, TaskCard],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss'
})
export class TaskList implements OnInit {
  private taskService = inject(TaskService);
  private notificationService = inject(NotificationService);
  private cdr = inject(ChangeDetectorRef);

  tasks: ITask[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.isLoading = true;

    this.taskService.getTasks().subscribe({
      next: (res: any) => {
        console.log('1. Search Raw Response:', res);

        if (Array.isArray(res)) {
          this.tasks = [...res];
        } else if (res && Array.isArray(res.data)) {
          this.tasks = [...res.data];
        } else if (res && Array.isArray(res.items)) {
          this.tasks = [...res.items];
        } else {
          this.tasks = [];
        }

        this.isLoading = false;
        console.log('2. Atanan Tasks Dizisi:', this.tasks);

        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Task error while loading tasks:', err);
        this.cdr.detectChanges();
      }
    });
  }

  async handleDeleteTask(taskId: string): Promise<void> {
    const confirmed = await this.notificationService.confirm('Are you sure you want to delete this task?');
    
    if (!confirmed) return;

    this.taskService.deleteTask(String(taskId)).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.notificationService.showSuccess('Task deleted successfully.');
          this.tasks = this.tasks.filter(t => t.id !== taskId);
          this.cdr.detectChanges(); // 👈 Silme sonrası ekranı güncelle
        } else {
          this.notificationService.showError(response.message || 'Delete failed.');
        }
      },
      error: () => {
        this.notificationService.showError('Could not delete task.');
      }
    });
  }
}