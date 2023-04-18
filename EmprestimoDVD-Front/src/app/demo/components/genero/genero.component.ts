import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';
import { get } from 'lodash';
import { Table } from 'primeng/table';
import { genero } from '../../api/genero';
import { GeneroService } from '../../service/genero.service';

@Component({
    templateUrl: './genero.component.html',
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
export class GeneroComponent implements OnInit {
    generos: genero[] = [];
    genero: genero = {};

    cols: any[] = [];

    _ = get;

    loading: boolean = true;
    generoDialog: boolean = false;
    submitted: boolean = false;

    @ViewChild('filter') filter!: ElementRef;

    constructor(
        private generoService: GeneroService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService
    ) {}

    ngOnInit() {
        this.cols = [
            {
                field: 'id',
                header: 'Id',
                type: 'number',
            },
            {
                field: 'descricao',
                header: 'Descrição',
                type: 'text',
            },
        ];
        this.fetchData();
    }

    fetchData() {
        this.loading = true;
        this.generoService.getAll().subscribe(
            (x) => {
                this.generos = x.object;
                this.messageService.add({
                    severity: 'success',
                    summary: `Gêneros carregados!`,
                    detail: `Registros carregados em: ${x.elapsed.elapsedMilliseconds}ms`,
                    life: 3000,
                });
                this.loading = false;
            },
            (error) => {
                this.messageService.add({
                    severity: 'error',
                    summary: `Erro ${error.code ?? error.status}`,
                    detail: `Não foi possível obter os registros! \n Erro: ${
                        error.title ?? error.message
                    }`,
                    life: 3000,
                });
                this.loading = false;
            }
        );
    }

    openNew() {
        this.genero = {};
        this.submitted = false;
        this.generoDialog = true;
    }

    hideDialog() {
        this.generoDialog = false;
        this.submitted = false;
    }

    saveGenero() {
        this.submitted = true;
        if (this.genero.descricao) {
            if (!this.genero.id) {
                this.generoService.createOrUpdate(this.genero).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `Gênero criado!`,
                            detail: `Seu genero foi criado em: ${x.elapsed.elapsedMilliseconds}ms`,
                            life: 3000,
                        });
                        this.generoDialog = false;
                        this.fetchData();
                    },
                    (error) => {
                        this.messageService.add({
                            severity: 'error',
                            summary: `Erro ${error.code ?? error.status}`,
                            detail: `Não foi possível salvar seu genero! \n Erro: ${
                                error.title ?? error.message
                            }`,
                            life: 3000,
                        });
                    }
                );
            } else {
                this.generoService
                    .createOrUpdate(this.genero, this.genero.id)
                    .subscribe(
                        (x) => {
                            this.messageService.add({
                                severity: 'success',
                                summary: `Registro atualizado!`,
                                detail: `O registro foi atualizado em: ${x.elapsed.elapsedMilliseconds}ms`,
                                life: 3000,
                            });
                            this.generoDialog = false;
                            this.fetchData();
                        },
                        (error) => {
                            this.messageService.add({
                                severity: 'error',
                                summary: `Erro ${error.code ?? error.status}`,
                                detail: `Não foi possível atualizar o registro! \n Erro: ${
                                    error.title ?? error.message
                                }`,
                                life: 3000,
                            });
                        }
                    );
            }
        }
    }

    editGenero(pessoaSelected: genero) {
        this.genero = { ...pessoaSelected };
        this.generoDialog = true;
    }

    deleteGenero(pessoaSelected: genero) {
        this.confirmationService.confirm({
            message: `Você tem certeza que deseja excluir o registro ${pessoaSelected.descricao}?`,
            header: 'Confirmar operação',
            acceptLabel: 'Sim',
            rejectLabel: 'Não',
            accept: () => {
                this.generoService.delete(pessoaSelected.id!).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `Gênero excluído!`,
                            detail: `O registro foi excluído em: ${x.elapsed.elapsedMilliseconds}ms`,
                            life: 3000,
                        });
                        this.fetchData();
                    },
                    (error) => {
                        this.messageService.add({
                            severity: 'error',
                            summary: `Erro ${error.code ?? error.status}`,
                            detail: `Não foi possível excluir o registro! \n Erro: ${
                                error.title ?? error.message
                            }`,
                            life: 3000,
                        });
                    }
                );
            },
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
