import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { pessoa } from '../api/pessoa';

@Injectable()
export class PessoaService extends BaseService<pessoa, pessoa> {
    constructor(http: HttpClient) {
        super(http, 'pessoa');
    }
}
