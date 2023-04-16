import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppLayoutModule } from './layout/app.layout.module';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { PessoaService } from './demo/service/pessoa.service';
import { GeneroService } from './demo/service/genero.service';
import { AmigoService } from './demo/service/amigo.service';

@NgModule({
    declarations: [AppComponent, NotfoundComponent],
    imports: [AppRoutingModule, AppLayoutModule],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        PessoaService,
        GeneroService,
        AmigoService
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
