import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Search } from '../Inteface/search';
import { APIResult } from '../Inteface/general';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { NzNotificationService } from 'ng-zorro-antd/notification';
@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private baseUrl = environment.baseUrl;
  constructor(private http:HttpClient,
    private notification: NzNotificationService,
  ) { }
  
  private getHttpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'true',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Accept, X-Custom-Header, Upgrade-Insecure-Requests',
      }),
      withCredentials: true,
    };
  }
  private getHttpOptionsForFormData() {
    return {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'true',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Accept, X-Custom-Header, Upgrade-Insecure-Requests'
      })
      , withCredentials: true
    };
  }
  

  addItems(addData: any): Observable<any> {
    var httpOptionsToUse = this.getHttpOptions();
    return this.http
      .post(this.baseUrl + `api/Search/AddSearch`, addData, httpOptionsToUse)
      .pipe((response: any) => {
        return response;
      });
  }
  SearchList() {
    return this.http.get<APIResult<Search[]>>(
      environment.baseUrl + 'api/Search/GetAll'
    );
  }
 
  SearchItems(searchCriteria:string): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();

    const url = `${this.baseUrl}api/Search/SearchItems?searchCriteria=${searchCriteria}`;
    return this.http
        .get(url, httpOptionsToUse)
        .pipe((response: any) => {
            return response;
        });
  }
  
 
}
