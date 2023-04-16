import { pessoa } from './pessoa';

export interface amigo extends pessoa {
    numTelefone?: number;
    email?: string;
    endereco?: string;
    dataNascimento?: Date;
}
