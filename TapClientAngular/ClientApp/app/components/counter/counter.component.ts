import { Component } from '@angular/core';

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent {
    public currentCount = 0;
    public maxSize: number = 3;
    public totalItems: number = 100;
    public currentPage: number = 1;
    public numPages: number = 100;
    public itemsPerPage: number=10;

    public incrementCounter() {
        this.currentCount++;
    }
}
