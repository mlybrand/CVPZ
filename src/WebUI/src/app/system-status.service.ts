import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class SystemStatusService {

  constructor(private http: Http) { }

  getStatusProfileService(): Observable<boolean> {
    return this.http.get('http://localhost:5002/api/health/ping')
      .map((res: Response) => res.text() === 'pong')
      .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
  }
}
