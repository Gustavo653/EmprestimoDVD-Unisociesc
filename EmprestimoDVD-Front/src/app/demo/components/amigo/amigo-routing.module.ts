import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AmigoComponent } from './amigo.component';

@NgModule({
    imports: [RouterModule.forChild([{ path: '', component: AmigoComponent }])],
    exports: [RouterModule],
})
export class AmigoRoutingModule {}
