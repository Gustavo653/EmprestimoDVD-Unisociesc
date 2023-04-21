import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EmprestimoComponent } from './emprestimo.component';

@NgModule({
    imports: [
        RouterModule.forChild([{ path: '', component: EmprestimoComponent }]),
    ],
    exports: [RouterModule],
})
export class EmprestimoRoutingModule {}
