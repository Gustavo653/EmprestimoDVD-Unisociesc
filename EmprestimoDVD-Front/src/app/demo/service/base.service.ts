import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { base } from '../api/base';

@Injectable()
export class BaseService<T> {
    constructor(
        private http: HttpClient,
        @Inject('apiUrl') private apiUrl: string
    ) {}

    getAll(): Observable<base<T[]>> {
        return this.http.get<base<T[]>>(
            `${environment.stringApiBase}/${this.apiUrl}/getall`
        );
    }

    getById(id: number): Observable<base<T>> {
        return this.http.get<base<T>>(
            `${environment.stringApiBase}/${this.apiUrl}/getbyid?id=${id}`
        );
    }

    createOrUpdate(entity: T, id?: number): Observable<base<T>> {
        return this.http.post<base<T>>(
            `${environment.stringApiBase}/${this.apiUrl}/createorupdate${
                id ? '?id=' + id : ''
            }`,
            entity
        );
    }

    delete(id: number): Observable<base<T>> {
        return this.http.delete<base<T>>(
            `${environment.stringApiBase}/${this.apiUrl}/delete?id=${id}`
        );
    }
}
