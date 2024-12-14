import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUser } from '../models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'http://localhost:5013/api';
  
  constructor(private http: HttpClient) {}
  
  addUser(user: IUser): Observable<IUser> {
    return this.http.post<IUser>(`${this.baseUrl}/user`, user);
  }
  
  getUser(id: number): Observable<IUser> {
    return this.http.get<IUser>(`${this.baseUrl}/user/${id}`);
  }
}