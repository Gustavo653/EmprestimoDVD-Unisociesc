import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PessoaComponent } from './pessoa.component';

@NgModule({
    imports: [
        RouterModule.forChild([{ path: '', component: PessoaComponent }]),
    ],
    exports: [RouterModule],
})
export class PessoaRoutingModule {}
