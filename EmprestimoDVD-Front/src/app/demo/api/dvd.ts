export interface dvd {
    id?: number;
    titulo?: string;
    sinopse?: string;
    idadeMinima?: number;
    idArtistaPrincipal?: number;
    idDiretor?: number;
    idGenero?: number;
}

export interface dvdGet {
    id?: number;
    titulo?: string;
    sinopse?: string;
    idadeMinima?: number;
    artistaPrincipal?: {
        nome: string;
        id: number;
    };
    diretor?: {
        nome: string;
        id: number;
    };
    genero?: {
        descricao: string;
        id: number;
    };
}
