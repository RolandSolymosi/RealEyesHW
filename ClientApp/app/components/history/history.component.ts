import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'history',
    templateUrl: './history.component.html'
})
export class HistoryComponent {
    public exchangeRates: ExchangeRate[];
    public currencies: Currency[] = [];
    public selectedCurrency: Currency;

    public lineChart: LineChart;

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) {
        http.get(`${originUrl}/api/Exchanges/Latest`).subscribe(result => {
            this.currencies = result.json() as Currency[];

            if (this.currencies.length > 0)
                this.selectedCurrency = this.currencies[0];
            this.refreshChart();
        });
    }

    public refreshChart(): void {
        if (this.selectedCurrency == null)
            return;

        this.lineChart = null;
        this.http.get(`${this.originUrl}/api/Exchanges/Currency/${this.selectedCurrency.name}`).subscribe(result => {
            this.exchangeRates = result.json() as ExchangeRate[];
            this.lineChart = new LineChart([{
                data: this.exchangeRates.map(e => e.rate),
                label: this.selectedCurrency.name
            }],
                this.exchangeRates.map(e => e.date.substring(0, 10)));

        });
    }
}

export class LineChart {
    constructor(data, label) {
        this.lineChartData = data;
        this.lineChartLabels = label;
    }
    public lineChartData: { data: number[], label: string }[] = [];
    public lineChartLabels: string[] = [];
    public lineChartOptions: any = {
        responsive: true
    };
    public lineChartColors: Array<any> = [{ // dark grey
        backgroundColor: 'rgba(77,83,96,0.2)',
        borderColor: 'rgba(77,83,96,1)',
        pointBackgroundColor: 'rgba(77,83,96,1)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(77,83,96,1)'
    }];
    public lineChartType: string = 'line';

    public chartHovered(e: any): void {
        console.log(e);
    }
    public chartClicked(e: any): void {
        console.log(e);
    }
}

interface ExchangeRate {
    date: string;
    rate: number;
}

interface Currency {
    name: string;
    rate: number;
}
