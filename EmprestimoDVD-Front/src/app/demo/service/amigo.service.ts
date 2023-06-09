import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { amigo } from '../api/amigo';

@Injectable()
export class AmigoService extends BaseService<amigo, amigo> {
    constructor(http: HttpClient) {
        super(http, 'amigo');
    }
}
