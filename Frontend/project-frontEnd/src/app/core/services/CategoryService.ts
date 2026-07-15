import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../enviroments/environment.development';
import { IApiResponse } from '../../shared/models/api-response.model';
import { ICategory, ICategoryCreateDto } from '../../shared/models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = `${environment.apiUrl}/categories`;

  constructor(private http: HttpClient) {}

  getCategories(): Observable<IApiResponse<ICategory[]>> {
    return this.http.get<IApiResponse<ICategory[]>>(this.apiUrl);
  }

  createCategory(category: ICategoryCreateDto): Observable<IApiResponse<ICategory>> {
    return this.http.post<IApiResponse<ICategory>>(this.apiUrl, category);
  }

  deleteCategory(id: number): Observable<IApiResponse<any>> {
    return this.http.delete<IApiResponse<any>>(`${this.apiUrl}/${id}`);
  }
}