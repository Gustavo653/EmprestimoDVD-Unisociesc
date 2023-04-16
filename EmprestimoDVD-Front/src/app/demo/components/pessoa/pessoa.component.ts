import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { base } from '../../api/base';
import { get } from 'lodash';
import { Table } from 'primeng/table';
import { pessoa } from '../../api/pessoa';
import { PessoaService } from '../../service/pessoa.service';

@Component({
    templateUrl: './pessoa.component.html',
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
export class PessoaComponent implements OnInit {
    pessoas: pessoa[] = [];
    pessoa: pessoa = {};

    cols: any[] = [];

    _ = get;

    loading: boolean = true;
    pessoaDialog: boolean = false;
    submitted: boolean = false;

    @ViewChild('filter') filter!: ElementRef;

    constructor(
        private pessoaService: PessoaService,
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
                field: 'nome',
                header: 'Nome',
                type: 'text',
            },
        ];
        this.fetchData();
    }
    
    fetchData() {
        this.loading = true;
        this.pessoaService.getAll().subscribe(
            (x) => {
                this.pessoas = x.object;
                this.messageService.add({
                    severity: 'success',
                    summary: `Pessoas carregados!`,
                    detail: `Registros carregados em: ${x.elapsed.elapsedMilliseconds}ms`,
                    life: 3000,
                });
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
            }
        );
        this.loading = false;
    }

    openNew() {
        this.pessoa = {};
        this.submitted = false;
        this.pessoaDialog = true;
    }

    hideDialog() {
        this.pessoaDialog = false;
        this.submitted = false;
    }

    savePessoa() {
        console.log(this.pessoa);
        this.submitted = true;
        if (this.pessoa.nome) {
            if (!this.pessoa.id) {
                this.pessoaService.createOrUpdate(this.pessoa).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `Pessoa criado!`,
                            detail: `Seu pessoa foi criado em: ${x.elapsed.elapsedMilliseconds}ms`,
                            life: 3000,
                        });
                        this.pessoaDialog = false;
                        this.fetchData();
                    },
                    (error) => {
                        this.messageService.add({
                            severity: 'error',
                            summary: `Erro ${error.code ?? error.status}`,
                            detail: `Não foi possível salvar seu pessoa! \n Erro: ${
                                error.title ?? error.message
                            }`,
                            life: 3000,
                        });
                    }
                );
            } else {
                this.pessoaService
                    .createOrUpdate(this.pessoa, this.pessoa.id)
                    .subscribe(
                        (x) => {
                            this.messageService.add({
                                severity: 'success',
                                summary: `Registro atualizado!`,
                                detail: `O registro foi atualizado em: ${x.elapsed.elapsedMilliseconds}ms`,
                                life: 3000,
                            });
                            this.pessoaDialog = false;
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

    editPessoa(pessoaSelected: pessoa) {
        this.pessoa = { ...pessoaSelected };
        this.pessoaDialog = true;
    }

    deletePessoa(pessoaSelected: pessoa) {
        this.confirmationService.confirm({
            message: `Você tem certeza que deseja excluir o registro ${pessoaSelected.nome}?`,
            header: 'Confirmar operação',
            acceptLabel: 'Sim',
            rejectLabel: 'Não',
            accept: () => {
                this.pessoaService.delete(pessoaSelected.id!).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `Pessoa excluído!`,
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
