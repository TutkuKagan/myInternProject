export interface ITask {
  id: string;
  title: string;
  status: number;
  priority: number;
  description?: string;
  isCompleted: boolean;
  dueDate?: string;
  categoryId: number;
  categoryName?: string;
  userId: number;
  attachmentCount?: number;
  commentCount?: number;

  createdAt :string;
  updatedAt :string;
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

export interface ITaskAttachment {
  id: string;
  taskId: string;
  fileName: string;
  fileSize?: number;
  fileType: string;
  fileUrl: string;
  uploadedAt: string;
}

export interface ITaskComment {
  id: string;
  taskId: string;
  userId: string;
  userName?: string;
  comment?: string;
  content?: string;
  createdAt: string;
}

export interface ICreateComment {
  taskId: string;
  comment: string;
}