import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { TaskService } from '../../../core/services/TaskService';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './task-form.html',
  styleUrl: './task-form.scss'
})
export class TaskForm implements OnInit {
  private fb = inject(FormBuilder);
  private taskService = inject(TaskService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  taskForm!: FormGroup;
  isLoading = false;
  

  isEditMode = false;
  taskId: string | null = null;

 ngOnInit(): void {
  const today = new Date().toISOString().split('T')[0];

  this.taskForm = this.fb.group({
    title: ['', [Validators.required]],
    description: ['', [Validators.required]],
    priority: [1, [Validators.required]],
    status: [0, [Validators.required]],
    dueDate: [today, [Validators.required]]
  });

  this.taskId = this.route.snapshot.paramMap.get('id');
  if (this.taskId) {
    this.isEditMode = true;
    this.loadTaskData(this.taskId);
  }
}

  loadTaskData(id: string): void {
    this.taskService.getTaskById(id).subscribe({
      next: (res: any) => {
        const task = res.data || res;
        if (task) {
          this.taskForm.patchValue({
            title: task.title,
            description: task.description,
            priority: task.priority,
            status: task.status,
            dueDate: task.dueDate
          });
        }
      },
      error: (err) => console.error('Task detail cannot be loaded:', err)
    });
  }

  onSubmit(): void {
  if (this.taskForm.invalid) {
    this.taskForm.markAllAsTouched();
    return;
  }

  this.isLoading = true;
  const formValues = this.taskForm.value;

  const priorityStringMap: { [key: number]: string } = {
    0: 'Low',
    1: 'Medium',
    2: 'High'
  };

  if (this.isEditMode && this.taskId) {

    // 1. EDIT (UPDATE) MODE

    const updatePayload = {
      title: formValues.title,
      description: formValues.description,
      priority: Number(formValues.priority),
      status: Number(formValues.status),
      dueDate: new Date(formValues.dueDate).toISOString(),
      categoryId: "7112f0f9-bf15-499b-9700-f71ea37b64d6"
    };

    this.taskService.updateTask(this.taskId, updatePayload).subscribe({
      next: () => {
        this.isLoading = false;
        this.router.navigate(['/tasks']);
      },
      error: (err) => {
        console.error('Update error:', err);
        this.isLoading = false;
      }
    });

  } else {

    // 2. CREATE MODE PAYLOAD

    const createPayload = {
      title: formValues.title,
      description: formValues.description,
      priority: Number(formValues.priority),
      status: Number(formValues.status),
      dueDate: new Date(formValues.dueDate).toISOString(),
      categoryId: "7112f0f9-bf15-499b-9700-f71ea37b64d6"
    };

    this.taskService.createTask(createPayload).subscribe({
      next: () => {
        this.isLoading = false;
        this.router.navigate(['/tasks']);
      },
      error: (err) => {
        console.error('Create error:', err);
        this.isLoading = false;
      }
    });
  }
}
}