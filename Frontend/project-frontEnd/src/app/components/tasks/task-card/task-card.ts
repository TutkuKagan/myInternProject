import { Component, Input, Output, EventEmitter , ChangeDetectionStrategy} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ITask } from '../../../shared/models/task.model';


@Component({
  selector: 'app-task-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './task-card.html',
  styleUrl: './task-card.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TaskCard {
  @Input() task!: any;
  @Output() deleteRequested = new EventEmitter<string>();

  onDelete(): void {
    if (this.task?.id) {
      this.deleteRequested.emit(String(this.task.id));
    }
  }

  get dueDateStatus(): 'overdue' | 'due-soon' | 'normal' {
    if (!this.task?.dueDate || Number(this.task.status) === 2) return 'normal';

    const now = new Date().getTime();
    const due = new Date(this.task.dueDate).getTime();
    const diffHours = (due - now) / (1000 * 60 * 60);

    if (diffHours < 0) return 'overdue';
    if (diffHours <= 24) return 'due-soon';
    return 'normal';
  }

  get progressPercentage(): number {
    switch (Number(this.task?.status)) {
      case 0: return 15;
      case 1: return 50;
      case 2: return 100;
      default: return 0;
    }
  }

  getPriorityLabel(priority: number | string): string {
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