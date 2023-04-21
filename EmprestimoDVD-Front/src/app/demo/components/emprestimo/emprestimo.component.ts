import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';
import { dvdGet } from '../../api/dvd';
import { DVDService } from '../../service/dvd.service';
import { Table } from 'primeng/table';
import { get } from 'lodash';
import { EmprestimoService } from '../../service/emprestimo.service';
import { itemDropdown } from '../../api/base';
import { AmigoService } from '../../service/amigo.service';

@Component({
    templateUrl: './emprestimo.component.html',
    providers: [MessageService, ConfirmationService],
    styles: [
        `
            :host ::ng-deep .p-frozen-column {
                font-weight: bold;
            }

            :host ::ng-deep .p-datatable-frozen-tbody {
                font-weight: bold;
            }

            :host ::ng-deep .p-progressbar {
                height: 0.5rem;
            }
        `,
    ],
})
export class EmprestimoComponent implements OnInit {
    dvds: any[] = [];
    historico: any[] = [];
    loading: boolean = false;
    cols: any[] = [];
    colsHistorico: any[] = [];

    emprestimoDialog: boolean = false;
    submitted: boolean = false;
    dvdNome: string = '';

    amigos: itemDropdown[] = [];

    emprestimoDVDDTO: any = {
        idDVD: 0,
        idAmigo: 0,
    };

    _ = get;

    @ViewChild('filter') filter!: ElementRef;

    constructor(
        private amigoService: AmigoService,
        private emprestimoService: EmprestimoService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {}

    ngOnInit() {
        this.cols = [
            {
                field: 'titulo',
                header: 'Título',
                type: 'text',
            },
            {
                field: 'sinopse',
                header: 'Sinopse',
                type: 'text',
            },
            {
                field: 'idadeMinima',
                header: 'Idade Mínima',
                type: 'text',
            },
            {
                field: 'genero.descricao',
                header: 'Gênero',
                type: 'text',
            },
            {
                field: 'diretor.nome',
                header: 'Diretor',
                type: 'text',
            },
            {
                field: 'artistaPrincipal.nome',
                header: 'Artista Principal',
                type: 'text',
            },
            {
                field: 'amigo',
                header: 'Amigo',
                type: 'text',
            },
        ];
        this.colsHistorico = [
            {
                field: 'titulo',
                header: 'Título',
                type: 'text',
            },
            {
                field: 'amigo',
                header: 'Amigo',
                type: 'text',
            },
            {
                field: 'dataEmprestimo',
                header: 'Data Empréstimo',
                type: 'date',
                date: true,
                format: `dd/MM/yyyy HH:mm:ss`,
            },
            {
                field: 'dataDevolver',
                header: 'Data Devolver',
                type: 'date',
                date: true,
                format: `dd/MM/yyyy HH:mm:ss`,
            },
        ];
        this.fetchData();
    }

    fetchData() {
        this.loading = true;
        this.amigos.length = 0;
        this.amigoService.getAll().subscribe((x) => {
            x.object.forEach((element) => {
                this.amigos.push({
                    code: element.id!,
                    name: element.nome!,
                });
            });
        });
        this.emprestimoService.buscaDVDEmprestimo().subscribe(
            (x) => {
                this.dvds = x.object;
                this.messageService.add({
                    severity: 'success',
                    summary: `DVDs carregados!`,
                    detail: `Registros carregados em: ${x.elapsed.elapsedMilliseconds}ms`,
                    life: 3000,
                });
                this.loading = false;
            },
            (error) => {
                this.messageService.add({
                    severity: 'error',
                    summary: `Erro ${error.error.code}`,
                    detail: `Não foi possível obter os registros! \n Erro: ${error.error.error.message}`,
                    life: 3000,
                });
                this.loading = false;
            }
        );
        this.emprestimoService.historicoEmprestimo().subscribe(
            (x) => {
                this.historico = x.object;
                this.messageService.add({
                    severity: 'success',
                    summary: `Histórico carregado!`,
                    detail: `Registros carregados em: ${x.elapsed.elapsedMilliseconds}ms`,
                    life: 3000,
                });
                this.loading = false;
            },
            (error) => {
                this.messageService.add({
                    severity: 'error',
                    summary: `Erro ${error.error.code}`,
                    detail: `Não foi possível obter os registros! \n Erro: ${error.error.error.message}`,
                    life: 3000,
                });
                this.loading = false;
            }
        );
    }

    openEmprestaDVD(dvd: any) {
        this.submitted = false;
        this.emprestimoDialog = true;
        this.dvdNome = dvd.titulo;
        this.emprestimoDVDDTO.idDVD = dvd.id;
    }

    emprestaDVD() {
        if (this.emprestimoDVDDTO.idDVD && this.emprestimoDVDDTO.idAmigo) {
            this.submitted = true;
            this.confirmationService.confirm({
                header: 'Emprestar DVD',
                message: `Você tem certeza que deseja marcar como emprestado o DVD ${this.dvdNome}?`,
                acceptLabel: 'Sim',
                rejectLabel: 'Não',
                accept: () =>
                    this.emprestimoService
                        .emprestaDVD(
                            this.emprestimoDVDDTO.idDVD,
                            this.emprestimoDVDDTO.idAmigo
                        )
                        .subscribe(
                            (x) => {
                                this.fetchData();
                                this.emprestimoDialog = false;
                            },
                            (error) => {
                                this.messageService.add({
                                    severity: 'error',
                                    summary: `Erro ${error.error.code}`,
                                    detail: `Não foi possível salvar o registro! \n Erro: ${error.error.error.message}`,
                                    life: 3000,
                                });
                            }
                        ),
            });
        }
    }

    devolveDVD(dvd: any) {
        this.confirmationService.confirm({
            header: 'Devolver DVD',
            message: `Você tem certeza que deseja marcar como devolvido o DVD ${dvd.titulo}?`,
            acceptLabel: 'Sim',
            rejectLabel: 'Não',
            accept: () =>
                this.emprestimoService.devolveDVD(dvd.id).subscribe(
                    (x) => {
                        this.fetchData();
                    },
                    (error) => {
                        this.messageService.add({
                            severity: 'error',
                            summary: `Erro ${error.error.code}`,
                            detail: `Não foi possível salvar o registro! \n Erro: ${error.error.error.message}`,
                            life: 3000,
                        });
                    }
                ),
        });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal(
            (event.target as HTMLInputElement).value,
            'contains'
        );
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }
}
