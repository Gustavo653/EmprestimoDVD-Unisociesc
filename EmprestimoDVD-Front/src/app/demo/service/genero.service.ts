import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { genero } from '../api/genero';

@Injectable()
export class GeneroService extends BaseService<genero, genero> {
    constructor(http: HttpClient) {
        super(http, 'genero');
    }
}
