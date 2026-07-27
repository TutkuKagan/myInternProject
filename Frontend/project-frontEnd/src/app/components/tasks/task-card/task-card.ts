import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-task-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './task-card.html',
  styleUrl: './task-card.scss'
})
export class TaskCard {
  @Input() task: any;
  @Output() deleteRequested = new EventEmitter<string>();

  onDelete(): void {
    if (this.task?.id) {
      this.deleteRequested.emit(this.task.id);
    }
  }

  getStatusLabel(status: number | string): string {
    switch (Number(status)) {
      case 0: return 'Todo';
      case 1: return 'In Progress';
      case 2: return 'Done';
      default: return 'Pending';
    }
  }

  getStatusClass(status: number | string): string {
    return this.getStatusLabel(status).toLowerCase().replace(' ', '-');
  }

  getPriorityLabel(priority: number | string): string {
    switch (Number(priority)) {
      case 0: return 'Low';
      case 1: return 'Medium';
      case 2: return 'High';
      default: return 'Normal';
    }
  }

  getPriorityClass(priority: number | string): string {
    return this.getPriorityLabel(priority).toLowerCase();
  }
}