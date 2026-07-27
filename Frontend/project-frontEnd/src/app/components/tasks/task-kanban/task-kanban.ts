import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { 
  CdkDragDrop, 
  DragDropModule, 
  moveItemInArray, 
  transferArrayItem 
} from '@angular/cdk/drag-drop';

import { TaskService } from '../../../core/services/TaskService';
import { NotificationService } from '../../../core/services/NotificationService';
import { ITask } from '../../../shared/models/task.model';

@Component({
  selector: 'app-task-kanban',
  standalone: true,
  imports: [CommonModule, RouterModule, DragDropModule],
  templateUrl: './task-kanban.html',
  styleUrl: './task-kanban.scss'
})
export class TaskKanban implements OnInit {
  private taskService = inject(TaskService);
  private notificationService = inject(NotificationService);
  private cdr = inject(ChangeDetectorRef);

  isLoading: boolean = false;

  pendingTasks: ITask[] = [];
  inProgressTasks: ITask[] = [];
  completedTasks: ITask[] = [];
  cancelledTasks: ITask[] = [];

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.isLoading = true;
    this.taskService.getTasks().subscribe({
      next: (res: any) => {
        this.isLoading = false;
        let allTasks: ITask[] = [];

        if (Array.isArray(res)) {
          allTasks = res;
        } else if (res && Array.isArray(res.data)) {
          allTasks = res.data;
        } else if (res && Array.isArray(res.items)) {
          allTasks = res.items;
        }

        this.pendingTasks = allTasks.filter(t => Number(t.status) === 0);
        this.inProgressTasks = allTasks.filter(t => Number(t.status) === 1);
        this.completedTasks = allTasks.filter(t => Number(t.status) === 2);
        this.cancelledTasks = allTasks.filter(t => Number(t.status) === 3);

        this.cdr.detectChanges();
      },
      error: () => {
        this.isLoading = false;
        this.notificationService.showError('Could not load Kanban tasks.');
        this.cdr.detectChanges();
      }
    });
  }

  onDrop(event: CdkDragDrop<ITask[]>, newStatus: number): void {
    if (event.previousContainer === event.container) {
      // Aynı sütun içinde sıra değiştiyse
      moveItemInArray(
        event.container.data, 
        event.previousIndex, 
        event.currentIndex
      );
    } else {
      const movedTask = event.previousContainer.data[event.previousIndex];
      const oldStatus = movedTask.status;

      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );

      movedTask.status = newStatus;

      const updatePayload = {
        title: movedTask.title,
        description: movedTask.description || '',
        priority: Number(movedTask.priority),
        status: newStatus,
        dueDate: movedTask.dueDate ? new Date(movedTask.dueDate).toISOString() : new Date().toISOString()
      };

      this.taskService.updateTask(movedTask.id, updatePayload).subscribe({
        next: () => {
          this.notificationService.showSuccess(`Task status updated!`);
        },
        error: (err) => {
          console.error('Status update failed:', err);
          this.notificationService.showError('Could not update status on server.');
          
          movedTask.status = oldStatus;
          this.loadTasks();
        }
      });
    }
  }

  getPriorityName(priority: number): string {
    switch (Number(priority)) {
      case 1: return 'Low';
      case 2: return 'Normal';
      case 3: return 'High';
      case 4: return 'Urgent';
      case 5: return 'Critical';
      default: return 'Normal';
    }
  }
}