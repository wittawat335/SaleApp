import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private urlApi: string = environment.endpoint + 'Menu';

  constructor(private http: HttpClient) {}

  GetList(userId: number): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}/${userId}`);
  }
}
