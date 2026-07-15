import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../enviroments/environment.development';
import { IApiResponse } from '../../shared/models/api-response.model';
import { ITask, ITaskCreateDto } from '../../shared/models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = `${environment.apiUrl}/tasks`;

  constructor(private http: HttpClient) {}


  getTasks(): Observable<IApiResponse<ITask[]>> {
    return this.http.get<IApiResponse<ITask[]>>(this.apiUrl);
  }


  getTaskById(id: number): Observable<IApiResponse<ITask>> {
    return this.http.get<IApiResponse<ITask>>(`${this.apiUrl}/${id}`);
  }


  createTask(task: ITaskCreateDto): Observable<IApiResponse<ITask>> {
    return this.http.post<IApiResponse<ITask>>(this.apiUrl, task);
  }


  updateTask(id: number, task: ITask): Observable<IApiResponse<ITask>> {
    return this.http.put<IApiResponse<ITask>>(`${this.apiUrl}/${id}`, task);
  }


  deleteTask(id: number): Observable<IApiResponse<any>> {
    return this.http.delete<IApiResponse<any>>(`${this.apiUrl}/${id}`);
  }
}