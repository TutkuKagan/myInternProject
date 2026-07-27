import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TaskService } from '../../../core/services/TaskService';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private taskService = inject(TaskService);
  private cdr = inject(ChangeDetectorRef);

  totalTasks: number = 0;
  completedTasks: number = 0;
  pendingTasks: number = 0;
  inProgressTasks: number = 0;
  isLoading: boolean = false;

  ngOnInit(): void {
    this.fetchMetrics();
  }

  fetchMetrics(): void {
    this.isLoading = true;

    this.taskService.getTasks().subscribe({
      next: (res: any) => {
        this.isLoading = false;
        
        let tasks: any[] = [];
        if (Array.isArray(res)) {
          tasks = res;
        } else if (res && Array.isArray(res.data)) {
          tasks = res.data;
        } else if (res && Array.isArray(res.items)) {
          tasks = res.items;
        }

        console.log('Dashboard Tasks:', tasks);

        this.totalTasks = tasks.length;

        this.pendingTasks = tasks.filter((t: any) => t.status === 0).length;
        this.inProgressTasks = tasks.filter((t: any) => t.status === 1).length;
        this.completedTasks = tasks.filter((t: any) => t.status === 2).length;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Dashboard metrics error:', err);
        this.cdr.detectChanges();
      }
    });
  }
}