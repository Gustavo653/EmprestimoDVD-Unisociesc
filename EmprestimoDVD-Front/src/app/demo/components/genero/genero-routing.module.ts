import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { GeneroComponent } from './genero.component';

@NgModule({
    imports: [
        RouterModule.forChild([{ path: '', component: GeneroComponent }]),
    ],
    exports: [RouterModule],
})
export class GeneroRoutingModule {}
