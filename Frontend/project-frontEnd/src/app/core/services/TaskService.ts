import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../enviroments/environment.development';
import { IApiResponse } from '../../shared/models/api-response.model';
import { ITask, ITaskCreateDto } from '../../shared/models/task.model';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = `http://localhost:5020/api/task`;

  constructor(private http: HttpClient) {}

  getTasks(queryDto?: any): Observable<any> {
  const options = queryDto && Object.keys(queryDto).length > 0 ? { params: queryDto } : {};
  return this.http.get<any>(`${this.apiUrl}/search`, options);
}

  getTaskById(id: string): Observable<IApiResponse<ITask>> {
    return this.http.get<IApiResponse<ITask>>(`${this.apiUrl}/getById?id=${id}`);
  }

  createTask(taskData: Partial<ITask> | any): Observable<any> {
  return this.http.post<any>(`${this.apiUrl}/createTask`, taskData);
}

  updateTask(id: string, taskData: any): Observable<any> {
  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  return this.http.put<any>(`${this.apiUrl}/updateTask?id=${id}`, taskData, { headers });
}

  deleteTask(id: string): Observable<IApiResponse<any>> {
    return this.http.delete<IApiResponse<any>>(`${this.apiUrl}/delete?id=${id}`);
  }

  getStatistics(): Observable<IApiResponse<any>> {
    return this.http.get<IApiResponse<any>>(`${this.apiUrl}/statistics`);
  }
}