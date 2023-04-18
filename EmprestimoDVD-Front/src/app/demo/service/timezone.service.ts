import { Injectable } from '@angular/core';

@Injectable()
export class TimeZoneService {
    constructor() {}
    public convertToTimeZone(
        date: Date,
        timeZone: string = 'America/Sao_Paulo'
    ): Date {
        date = new Date(date);
        const dateUTC = Date.UTC(
            date.getFullYear(),
            date.getMonth(),
            date.getDate(),
            date.getHours(),
            date.getMinutes(),
            date.getSeconds()
        );
        const convertedDate = new Date(
            dateUTC - this.getTimeZoneOffset(date, timeZone)
        );
        return convertedDate;
    }

    private getTimeZoneOffset(date: Date, timeZone: string): number {
        const timeZoneOffset = new Date(
            date.toLocaleString('en-US', { timeZone })
        ).getTimezoneOffset();
        const utcOffset = new Date().getTimezoneOffset();
        return (utcOffset - timeZoneOffset) * 60 * 1000;
    }
}
