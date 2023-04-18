import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MessageService, ConfirmationService } from 'primeng/api';
import { get } from 'lodash';
import { Table } from 'primeng/table';
import { dvd, dvdGet } from '../../api/dvd';
import { DVDService } from '../../service/dvd.service';
import { PessoaService } from '../../service/pessoa.service';
import { GeneroService } from '../../service/genero.service';
import { itemDropdown } from '../../api/base';

@Component({
    templateUrl: './dvd.component.html',
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

            :host ::ng-deep .p-dropdown-panel {
                z-index: 10002 !important;
            }
        `,
    ],
})
export class DVDComponent implements OnInit {
    dvds: dvdGet[] = [];
    dvd: dvd = {};

    generos: itemDropdown[] = [];
    pessoas: itemDropdown[] = [];

    cols: any[] = [];

    _ = get;

    loading: boolean = true;
    dvdDialog: boolean = false;
    submitted: boolean = false;

    @ViewChild('filter') filter!: ElementRef;

    constructor(
        private dvdService: DVDService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService,
        private pessoaService: PessoaService,
        private generoService: GeneroService
    ) {}

    ngOnInit() {
        this.cols = [
            {
                field: 'id',
                header: 'Id',
                type: 'number',
            },
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
        ];
        this.fetchData();
    }

    fetchData() {
        this.loading = true;
        this.pessoas.length = 0;
        this.generos.length = 0;
        this.pessoaService.getAll().subscribe((x) => {
            x.object.forEach((element) => {
                this.pessoas.push({
                    code: element.id!,
                    name: element.nome!,
                });
            });
        });
        this.generoService.getAll().subscribe((x) => {
            x.object.forEach((element) => {
                this.generos.push({
                    code: element.id!,
                    name: element.descricao!,
                });
            });
        });
        this.dvdService.getAll().subscribe(
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
        this.dvd = {};
        this.submitted = false;
        this.dvdDialog = true;
    }

    hideDialog() {
        this.dvdDialog = false;
        this.submitted = false;
    }

    saveDVD() {
        console.log(this.dvd);
        this.submitted = true;
        if (
            this.dvd.titulo &&
            this.dvd.sinopse &&
            this.dvd.idadeMinima &&
            this.dvd.idGenero &&
            this.dvd.idDiretor &&
            this.dvd.idArtistaPrincipal &&
            this.dvd.idadeMinima >= 0 &&
            this.dvd.idadeMinima <= 18
        ) {
            if (!this.dvd.id) {
                this.dvdService.createOrUpdate(this.dvd).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `DVD criado!`,
                            detail: `Seu dvd foi criado em: ${x.elapsed.elapsedMilliseconds}ms`,
                            life: 3000,
                        });
                        this.dvdDialog = false;
                        this.fetchData();
                    },
                    (error) => {
                        this.messageService.add({
                            severity: 'error',
                            summary: `Erro ${error.code ?? error.status}`,
                            detail: `Não foi possível salvar seu dvd! \n Erro: ${
                                error.title ?? error.message
                            }`,
                            life: 3000,
                        });
                    }
                );
            } else {
                this.dvdService.createOrUpdate(this.dvd, this.dvd.id).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `Registro atualizado!`,
                            detail: `O registro foi atualizado em: ${x.elapsed.elapsedMilliseconds}ms`,
                            life: 3000,
                        });
                        this.dvdDialog = false;
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

    editDVD(dvdSelected: dvdGet) {
        this.dvd = { ...dvdSelected };
        this.dvd.idArtistaPrincipal = dvdSelected.artistaPrincipal?.id;
        this.dvd.idDiretor = dvdSelected.diretor?.id;
        this.dvd.idGenero = dvdSelected.genero?.id;
        this.dvdDialog = true;
    }

    deleteDVD(dvdSelected: dvd) {
        this.confirmationService.confirm({
            message: `Você tem certeza que deseja excluir o registro ${dvdSelected.titulo}?`,
            header: 'Confirmar operação',
            acceptLabel: 'Sim',
            rejectLabel: 'Não',
            accept: () => {
                this.dvdService.delete(dvdSelected.id!).subscribe(
                    (x) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: `DVD excluído!`,
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
