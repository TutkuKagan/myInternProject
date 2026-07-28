import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../enviroments/environment.development';
import { IApiResponse } from '../../shared/models/api-response.model';
import { HttpHeaders } from '@angular/common/http';
import { ITask, ITaskCreateDto, ITaskAttachment } from '../../shared/models/task.model';
import { ITaskComment, ICreateComment } from '../../shared/models/task.model';
import { CacheService } from '../services/cache';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = `http://localhost:5020/api/task`;

  constructor(private http: HttpClient,private cacheService: CacheService) {}

  getTasks(queryDto?: any): Observable<any> {
  const options = queryDto && Object.keys(queryDto).length > 0 ? { params: queryDto } : {};
  return this.http.get<any>(`${this.apiUrl}/search`, options);
}

  getTaskById(id: string): Observable<IApiResponse<ITask>> {
    return this.http.get<IApiResponse<ITask>>(`${this.apiUrl}/getById?id=${id}`);
  }

  createTask(taskData: Partial<ITask> | any): Observable<any> {
  return this.http.post<any>(`${this.apiUrl}/createTask`, taskData).pipe(
      tap(() => {
        this.cacheService.clearAll();
      })
    );
  };


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


uploadAttachment(taskId: string, file: File) {
  const formData = new FormData();
  formData.append('TaskItemId', taskId);
  formData.append('File', file);

  return this.http.post<ITaskAttachment>(`${this.apiUrl}/upload-attachment`, formData);
}

getAttachments(taskId: string) {
  return this.http.get<ITaskAttachment[]>(`${this.apiUrl}/attachments/${taskId}`);
}

deleteAttachment(taskId: string, attachmentId: string) {
  return this.http.delete(`${this.apiUrl}/attachments/${attachmentId}`);
}

getCommentsByTaskId(taskId: string): Observable<ITaskComment[]> {
  return this.http.get<ITaskComment[]>(`http://localhost:5020/api/comments/task/${taskId}`);
}

  addComment(dto: ICreateComment): Observable<ITaskComment> {
  return this.http.post<ITaskComment>('http://localhost:5020/api/comments/add', dto);
}

  deleteComment(commentId: string): Observable<void> {
  return this.http.delete<void>(`http://localhost:5020/api/comments/${commentId}`);
}
}