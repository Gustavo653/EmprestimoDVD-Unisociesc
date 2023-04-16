import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { pessoa } from '../api/pessoa';
import { base } from '../api/base';

@Injectable()
export class PessoaService extends BaseService<pessoa> {
    constructor(http: HttpClient) {
        super(http, 'pessoa');
    }
}
