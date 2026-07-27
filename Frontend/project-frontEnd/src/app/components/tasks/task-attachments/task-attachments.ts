import { Component, Input, OnInit, OnChanges, SimpleChanges, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../../core/services/TaskService';
import { NotificationService } from '../../../core/services/NotificationService';
import { ITaskAttachment } from '../../../shared/models/task.model';

@Component({
  selector: 'app-task-attachments',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-attachments.html',
  styleUrl: './task-attachments.scss'
})
export class TaskAttachments implements OnInit, OnChanges {
  @Input() taskId!: string;

  private taskService = inject(TaskService);
  private notificationService = inject(NotificationService);
  private cdr = inject(ChangeDetectorRef);

  attachments: ITaskAttachment[] = [];
  isUploading: boolean = false;
  isDragging: boolean = false;

  ngOnInit(): void {
    if (this.taskId) {
      this.loadAttachments();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['taskId'] && changes['taskId'].currentValue && !changes['taskId'].firstChange) {
      this.loadAttachments();
    }
  }

  loadAttachments(): void {
    if (!this.taskId) return;

    console.log('📎 Attachment yüklenen Task ID:', this.taskId);

    this.taskService.getAttachments(this.taskId).subscribe({
      next: (res: any) => {
        this.attachments = Array.isArray(res) ? res : res?.data || [];
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Attachment çekme hatası:', err);
        this.attachments = [];
        this.cdr.detectChanges();
      }
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.uploadFile(input.files[0]);
    }
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    this.isDragging = false;
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    this.isDragging = false;
    if (event.dataTransfer?.files && event.dataTransfer.files.length > 0) {
      this.uploadFile(event.dataTransfer.files[0]);
    }
  }

  uploadFile(file: File): void {
    if (!this.taskId) {
      this.notificationService.showError('Task ID missing.');
      return;
    }

    if (file.size > 10 * 1024 * 1024) {
      this.notificationService.showError('File size cannot exceed 10 MB.');
      return;
    }

    this.isUploading = true;
    this.taskService.uploadAttachment(this.taskId, file).subscribe({
      next: (newAttachment) => {
        this.isUploading = false;
        this.notificationService.showSuccess('File uploaded successfully!');
        if (newAttachment) {
          this.attachments.push(newAttachment);
        } else {
          this.loadAttachments();
        }
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isUploading = false;
        console.error('Upload error:', err);
        this.notificationService.showError('Could not upload file.');
        this.cdr.detectChanges();
      }
    });
  }

  deleteAttachment(attachmentId: string): void {
    this.taskService.deleteAttachment(this.taskId, attachmentId).subscribe({
      next: () => {
        this.attachments = this.attachments.filter(a => a.id !== attachmentId);
        this.notificationService.showSuccess('Attachment deleted.');
        this.cdr.detectChanges();
      },
      error: () => {
        this.notificationService.showError('Could not delete attachment.');
      }
    });
  }

formatFileSize(bytes?: number): string {
  if (bytes === undefined || bytes === null || isNaN(bytes)) return '0 B';
  const k = 1024;
  const sizes = ['B', 'KB', 'MB', 'GB'];
  const i = Math.floor(Math.log(bytes) / Math.log(k));
  return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
}
}