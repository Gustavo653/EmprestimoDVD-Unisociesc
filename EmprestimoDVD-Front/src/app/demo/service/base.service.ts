import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { base } from '../api/base';

@Injectable()
export class BaseService<TGet, TPost> {
    constructor(
        private http: HttpClient,
        @Inject('apiUrl') private apiUrl: string
    ) {}

    getAll(): Observable<base<TGet[]>> {
        return this.http.get<base<TGet[]>>(
            `${environment.stringApiBase}/${this.apiUrl}/getall`
        );
    }

    getById(id: number): Observable<base<TGet>> {
        return this.http.get<base<TGet>>(
            `${environment.stringApiBase}/${this.apiUrl}/getbyid?id=${id}`
        );
    }

    createOrUpdate(entity: TPost, id?: number): Observable<base<TPost>> {
        return this.http.post<base<TPost>>(
            `${environment.stringApiBase}/${this.apiUrl}/createorupdate${
                id ? '?id=' + id : ''
            }`,
            entity
        );
    }

    delete(id: number): Observable<base<TGet>> {
        return this.http.delete<base<TGet>>(
            `${environment.stringApiBase}/${this.apiUrl}/delete?id=${id}`
        );
    }
}
