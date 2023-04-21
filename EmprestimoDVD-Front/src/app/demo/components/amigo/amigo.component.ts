import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { base } from '../../api/base';
import { get } from 'lodash';
import { Table } from 'primeng/table';
import { amigo } from '../../api/amigo';
import { AmigoService } from '../../service/amigo.service';

@Component({
    templateUrl: './amigo.component.html',
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
export class AmigoComponent implements OnInit {
    amigos: amigo[] = [];
    amigo: amigo = {};

    cols: any[] = [];

    _ = get;

    loading: boolean = true;
    amigoDialog: boolean = false;
    submitted: boolean = false;

    @ViewChild('filter') filter!: ElementRef;

    constructor(
        private amigoService: AmigoService,
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
            {
                field: 'numTelefone',
                header: 'Número Telefone',
                type: 'text',
            },
            {
                field: 'email',
                header: 'E-mail',
                type: 'text',
            },
            {
                field: 'endereco',
                header: 'Endereço',
                type: 'text',
            },
            {
                field: 'dataNascimento',
                header: 'Data Nascimento',
                type: 'date',
                date: true,
                format: `dd/MM/yyyy`,
            },
        ];
        this.fetchData();
    }

    fetchData() {
        this.loading = true;
        this.amigoService.getAll().subscribe(
            (x) => {
                this.amigos = x.object;
                this.messageService.add({
                    severity: 'success',
                    summary: `Amigos carregados!`,
                    detail: `Registros carregados em: ${x.elapsed.elapsedMilliseconds}ms`,
                    life: 3000,
                });
                this.loading = false;
            },
            (error) => {
                this.messageService.add({
                    severity: 'error',
                    summary: `Erro ${error.error.code}`,
                    detail: `Não foi possível obter os registros! \n Erro: ${
                        error.error.error.message
                    }`,
                    life: 3000,
                });
                this.loading = false;
            }
        );
    }

    openNew() {
        this.amigo = {};
        this.submitted = false;
        this.amigoDialog = true;
    }

    hideDialog() {
        this.amigoDialog = false;
        this.submitted = false;
    }

    isEmailValid(email: string): boolean {
        const emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$/;
        return emailRegex.test(email);
    }

    saveAmigo() {
        this.submitted = true;
        if (
            this.amigo.nome &&
            this.amigo.numTelefone &&
            this.amigo.email &&
            this.amigo.dataNascimento &&
            this.isEmailValid(this.amigo.email)
        ) {
            this.amigo.dataNascimento = new Date(
                this.amigo.dataNascimento.getFullYear(),
                this.amigo.dataNascimento.getMonth(),
                this.amigo.dataNascimento.getDate()
            );
            if (!this.amigo.id) {
                this.amigoService.createOrUpdate(this.amigo).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `Amigo criado!`,
                            detail: `Seu amigo foi criado em: ${x.elapsed.elapsedMilliseconds}ms`,
                            life: 3000,
                        });
                        this.amigoDialog = false;
                        this.fetchData();
                    },
                    (error) => {
                        this.messageService.add({
                            severity: 'error',
                            summary: `Erro ${error.error.code}`,
                            detail: `Não foi possível salvar o registro! \n Erro: ${
                                error.error.error.message
                            }`,
                            life: 3000,
                        });
                    }
                );
            } else {
                this.amigoService
                    .createOrUpdate(this.amigo, this.amigo.id)
                    .subscribe(
                        (x) => {
                            this.messageService.add({
                                severity: 'success',
                                summary: `Registro atualizado!`,
                                detail: `O registro foi atualizado em: ${x.elapsed.elapsedMilliseconds}ms`,
                                life: 3000,
                            });
                            this.amigoDialog = false;
                            this.fetchData();
                        },
                        (error) => {
                            this.messageService.add({
                                severity: 'error',
                                summary: `Erro ${error.error.code}`,
                                detail: `Não foi possível atualizar o registro! \n Erro: ${
                                    error.error.error.message
                                }`,
                                life: 3000,
                            });
                        }
                    );
            }
        }
    }

    editAmigo(amigoSelected: amigo) {
        this.amigo = { ...amigoSelected };
        this.amigo.dataNascimento =
            this.amigo.dataNascimento != undefined
                ? new Date(this.amigo.dataNascimento)
                : undefined;
        this.amigoDialog = true;
    }

    deleteAmigo(amigoSelected: amigo) {
        this.confirmationService.confirm({
            message: `Você tem certeza que deseja excluir o registro ${amigoSelected.nome}?`,
            header: 'Confirmar operação',
            acceptLabel: 'Sim',
            rejectLabel: 'Não',
            accept: () => {
                this.amigoService.delete(amigoSelected.id!).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `Amigo excluído!`,
                            detail: `O registro foi excluído em: ${x.elapsed.elapsedMilliseconds}ms`,
                            life: 3000,
                        });
                        this.fetchData();
                    },
                    (error) => {
                        this.messageService.add({
                            severity: 'error',
                            summary: `Erro ${error.error.code}`,
                            detail: `Não foi possível excluir o registro! \n Erro: ${
                                error.error.error.message
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
