import { Component, OnInit, inject, ChangeDetectorRef, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TaskCard } from '../task-card/task-card';
import { TaskService } from '../../../core/services/TaskService';
import { NotificationService } from '../../../core/services/NotificationService';
import { StorageService } from '../../../core/services/storage';
import { ITask } from '../../../shared/models/task.model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

interface ITaskFilterPreferences {
  statusFilter: string;
  priorityFilter: string;
  sortBy: string;
  pageSize: number;
}

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, TaskCard],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss'
})
export class TaskList implements OnInit {
  private taskService = inject(TaskService);
  private notificationService = inject(NotificationService);
  private storageService = inject(StorageService);
  private cdr = inject(ChangeDetectorRef);
  private destroyRef = inject(DestroyRef);

  allTasks: ITask[] = [];
  filteredTasks: ITask[] = [];
  paginatedTasks: ITask[] = [];

  isLoading: boolean = false;
  errorMessage: string = '';

  searchTerm: string = '';
  statusFilter: string = 'ALL';
  priorityFilter: string = 'ALL';
  sortBy: string = 'createdAt_desc';

  currentPage: number = 1;
  pageSize: number = 3;
  totalPages: number = 1;

  private readonly FILTER_STORAGE_KEY = 'task_list_user_filters';

  ngOnInit(): void {
    this.loadSavedFilters();
    this.loadTasks();
  }

  private loadSavedFilters(): void {
    const saved = this.storageService.getItem<ITaskFilterPreferences>(this.FILTER_STORAGE_KEY);
    if (saved) {
      this.statusFilter = saved.statusFilter ?? 'ALL';
      this.priorityFilter = saved.priorityFilter ?? 'ALL';
      this.sortBy = saved.sortBy ?? 'createdAt_desc';
      this.pageSize = saved.pageSize ?? 3;
    }
  }

  loadTasks(): void {
    this.isLoading = true;

    this.taskService.getTasks().pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (res: any) => {
        this.isLoading = false;
        
        if (Array.isArray(res)) {
          this.allTasks = res;
        } else if (res && Array.isArray(res.data)) {
          this.allTasks = res.data;
        } else if (res && Array.isArray(res.items)) {
          this.allTasks = res.items;
        } else {
          this.allTasks = [];
        }

        this.applyFilters();
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Task error while loading tasks:', err);
        this.cdr.detectChanges();
      }
    });
  }

  applyFilters(): void {
    this.storageService.setItem<ITaskFilterPreferences>(this.FILTER_STORAGE_KEY, {
      statusFilter: this.statusFilter,
      priorityFilter: this.priorityFilter,
      sortBy: this.sortBy,
      pageSize: this.pageSize
    });

    let tempTasks = [...this.allTasks];

    if (this.searchTerm.trim() !== '') {
      const term = this.searchTerm.toLowerCase();
      tempTasks = tempTasks.filter(task => 
        task.title?.toLowerCase().includes(term) || 
        task.description?.toLowerCase().includes(term)
      );
    }

    if (this.statusFilter !== 'ALL') {
      const statusNum = Number(this.statusFilter);
      tempTasks = tempTasks.filter(task => task.status === statusNum);
    }

    if (this.priorityFilter !== 'ALL') {
      const priorityNum = Number(this.priorityFilter);
      tempTasks = tempTasks.filter(task => task.priority === priorityNum);
    }

    tempTasks.sort((a, b) => {
      switch (this.sortBy) {
        case 'dueDate_asc':
          return new Date(a.dueDate || 0).getTime() - new Date(b.dueDate || 0).getTime();
        case 'dueDate_desc':
          return new Date(b.dueDate || 0).getTime() - new Date(a.dueDate || 0).getTime();
        case 'priority_desc':
          return b.priority - a.priority;
        case 'priority_asc':
          return a.priority - b.priority;
        case 'title_asc':
          return a.title.localeCompare(b.title);
        case 'createdAt_desc':
        default:
          return new Date(b.createdAt || 0).getTime() - new Date(a.createdAt || 0).getTime();
      }
    });

    this.filteredTasks = tempTasks;
    this.currentPage = 1;
    this.updatePagination();
  }

  updatePagination(): void {
    this.totalPages = Math.ceil(this.filteredTasks.length / this.pageSize) || 1;
    
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    
    this.paginatedTasks = this.filteredTasks.slice(startIndex, endIndex);
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updatePagination();
  }

  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  async handleDeleteTask(taskId: string): Promise<void> {
    const confirmed = await this.notificationService.confirm('Are you sure you want to delete this task?');
    if (!confirmed) return;

    this.taskService.deleteTask(String(taskId)).pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: () => {
        this.notificationService.showSuccess('Task deleted successfully.');
        this.allTasks = this.allTasks.filter(t => t.id !== taskId);
        this.applyFilters();
      },
      error: () => {
        this.notificationService.showError('Could not delete task.');
      }
    });
  }

  trackByTaskId(index: number, task: ITask): string {
  return task.id;
}
}