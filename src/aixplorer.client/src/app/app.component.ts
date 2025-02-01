import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    template: '<app-layout></app-layout>',
    styles: [],
    standalone: false
})
export class AppComponent
{
    title = 'aixplorer.client';

    constructor()
    {
    }

    ngOnInit(): void
    {

        window.onbeforeunload = () =>
        {
            this.releaseSignalRResources();
        };
    }

    ngOnDestroy()
    {
        this.releaseSignalRResources();
    }

    private releaseSignalRResources()
    {
       
    }
}
