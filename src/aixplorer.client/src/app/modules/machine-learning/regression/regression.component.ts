import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-regression',
    templateUrl: './regression.component.html',
    styleUrls: ['./regression.component.css'],
})
export class RegressionComponent implements OnInit
{
    constructor(private router: Router)
    {
    }

    ngOnInit(): void
    {
        
    }

    ngOnDestroy()
    {
    }
}
