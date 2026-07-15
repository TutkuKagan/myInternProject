export interface ITask {
  id: number;
  title: string;
  description?: string;
  isCompleted: boolean;
  dueDate?: string;
  categoryId: number;
  categoryName?: string;
  userId: number;

  comments?: IComment[];
}

export interface IComment {
  id: number;
  text: string;
  createdDate: string;
  taskId: number;
  userId: number;
  userName?: string;
}

export interface ITaskCreateDto {
  title: string;
  description?: string;
  dueDate?: string;
  categoryId: number;
  userId: number;
}