import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
    model: any[] = [];

    constructor(public layoutService: LayoutService) {}

    ngOnInit() {
        this.model = [
            {
                label: 'Home',
                items: [
                    {
                        label: 'Empréstimo',
                        icon: 'pi pi-fw pi-book',
                        routerLink: ['/'],
                    },
                    {
                        label: 'Pessoa',
                        icon: 'pi pi-fw pi-user',
                        routerLink: ['/pessoa'],
                    },
                    {
                        label: 'Amigo',
                        icon: 'pi pi-fw pi-user',
                        routerLink: ['/amigo'],
                    },
                    {
                        label: 'Gênero',
                        icon: 'pi pi-fw pi-th-large',
                        routerLink: ['/genero'],
                    },
                    {
                        label: 'DVD',
                        icon: 'pi pi-fw pi-tablet',
                        routerLink: ['/dvd'],
                    },
                ],
            },
        ];
    }
}
