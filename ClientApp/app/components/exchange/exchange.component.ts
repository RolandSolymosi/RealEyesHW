import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'exchange',
    templateUrl: './exchange.component.html'
})
export class ExchangeComponent {
    public currencies: Currency[];
    public from: Currency;
    public to: Currency;
    public amount: number = 0;
    public result: number = 0;

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/Exchanges/Latest').subscribe(result => {
            this.currencies = result.json() as Currency[];
            if (this.currencies.length > 0) {
                this.from = this.currencies[0];
                this.to = this.currencies[0];
                this.recalculate();
            }
        });
    }

    public recalculate(): void {
        if (this.from == null || this.to == null || this.amount == null)
            return;

        this.result = (this.amount / this.from.rate) * this.to.rate;
    }
}

interface Currency {
    name: string;
    rate: number;
}
