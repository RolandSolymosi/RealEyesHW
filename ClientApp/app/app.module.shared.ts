import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ExchangeComponent } from './components/exchange/exchange.component';
import { HistoryComponent } from './components/history/history.component';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        ExchangeComponent,
        HistoryComponent,
        HomeComponent
    ],
    imports: [
        FormsModule,
        ChartsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'exchange', component: ExchangeComponent },
            { path: 'history', component: HistoryComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
};
