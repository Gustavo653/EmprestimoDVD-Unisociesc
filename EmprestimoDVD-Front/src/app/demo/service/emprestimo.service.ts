import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { dvd, dvdGet } from '../api/dvd';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { base } from '../api/base';

@Injectable()
export class EmprestimoService {
    constructor(private http: HttpClient) {}

    buscaDVDEmprestimo(): Observable<base<any>> {
        return this.http.get<base<any>>(
            `${environment.stringApiBase}/emprestimo/BuscaDVDEmprestimo`
        );
    }

    historicoEmprestimo(): Observable<base<any>> {
        return this.http.get<base<any>>(
            `${environment.stringApiBase}/emprestimo/HistoricoEmprestimo`
        );
    }

    emprestaDVD(idDVD: number, idAmigo: number): Observable<base<any>> {
        return this.http.post<base<any>>(
            `${environment.stringApiBase}/emprestimo/EmprestaDVD`,
            { idDVD: idDVD, idAmigo: idAmigo }
        );
    }

    devolveDVD(idDVD: number): Observable<base<any>> {
        return this.http.post<base<any>>(
            `${environment.stringApiBase}/emprestimo/DevolveDVD`,
            { idDVD: idDVD }
        );
    }
}
