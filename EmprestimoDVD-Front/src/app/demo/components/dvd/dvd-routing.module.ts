import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DVDComponent } from './dvd.component';

@NgModule({
    imports: [
        RouterModule.forChild([{ path: '', component: DVDComponent }]),
    ],
    exports: [RouterModule],
})
export class DVDRoutingModule {}
