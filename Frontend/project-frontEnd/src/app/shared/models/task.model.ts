export interface ITask {
  id: number;
  title: string;
  description?: string;
  isCompleted: boolean;
  dueDate?: string;
  categoryId: number;
  categoryName?: string;
  userId: number;
}