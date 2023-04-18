import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { dvd, dvdGet } from '../api/dvd';

@Injectable()
export class DVDService extends BaseService<dvdGet, dvd> {
    constructor(http: HttpClient) {
        super(http, 'dvd');
    }
}
