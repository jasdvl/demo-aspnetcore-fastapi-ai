import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit, OnDestroy
{
    isSidebarOpen: boolean = false;
    
    constructor()
    {
    }

    ngOnInit(): void
    {
        
    }

    ngAfterViewInit()
    {
    }

    ngOnDestroy()
    {
    }

    toggleSidebar()
    {
        this.isSidebarOpen = !this.isSidebarOpen;
    }

    closeSidebar()
    {
        this.isSidebarOpen = false;
    }
}
