 import { ITask } from './task.model';
 
 export interface ICategory {
  id: number;
  name: string;
  description?: string;


  tasks?: ITask[];
}
