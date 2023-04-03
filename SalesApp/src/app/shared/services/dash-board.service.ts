import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class DashBoardService {
  private urlApi: string = environment.endpoint + 'DashBoard';

  constructor(private http: HttpClient) {}

  Summary(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.urlApi + '/Summary');
  }
}
