import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { AppLayoutComponent } from './layout/app.layout.component';

@NgModule({
    imports: [
        RouterModule.forRoot(
            [
                {
                    path: '',
                    component: AppLayoutComponent,
                    children: [
                        {
                            path: '',
                            loadChildren: () =>
                                import(
                                    './demo/components/emprestimo/emprestimo.module'
                                ).then((m) => m.EmprestimoModule),
                        },
                        {
                            path: 'pessoa',
                            loadChildren: () =>
                                import(
                                    './demo/components/pessoa/pessoa.module'
                                ).then((m) => m.PessoaModule),
                        },
                        {
                            path: 'genero',
                            loadChildren: () =>
                                import(
                                    './demo/components/genero/genero.module'
                                ).then((m) => m.GeneroModule),
                        },
                        {
                            path: 'amigo',
                            loadChildren: () =>
                                import(
                                    './demo/components/amigo/amigo.module'
                                ).then((m) => m.AmigoModule),
                        },
                        {
                            path: 'dvd',
                            loadChildren: () =>
                                import(
                                    './demo/components/dvd/dvd.module'
                                ).then((m) => m.DVDModule),
                        },
                    ],
                },
                {
                    path: 'auth',
                    loadChildren: () =>
                        import('./demo/components/auth/auth.module').then(
                            (m) => m.AuthModule
                        ),
                },
                {
                    path: 'landing',
                    loadChildren: () =>
                        import('./demo/components/landing/landing.module').then(
                            (m) => m.LandingModule
                        ),
                },
                { path: 'pages/notfound', component: NotfoundComponent },
                { path: '**', redirectTo: 'pages/notfound' },
            ],
            {
                scrollPositionRestoration: 'enabled',
                anchorScrolling: 'enabled',
                onSameUrlNavigation: 'reload',
            }
        ),
    ],
    exports: [RouterModule],
})
export class AppRoutingModule {}
