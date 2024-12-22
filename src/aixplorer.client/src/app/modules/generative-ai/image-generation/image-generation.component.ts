import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'app/core/services/rest-api.service';

@Component({
    selector: 'app-image-generation',
    templateUrl: './image-generation.component.html',
    styleUrls: ['./image-generation.component.css'],
})
export class ImageGenerationComponent implements OnInit
{
    textInputValue: string = '';

    constructor(private router: Router, private apiService: ApiService)
    {
    }

    ngOnInit(): void
    {
        
    }

    ngOnDestroy()
    {
    }

    onStartGeneration()
    {
        this.apiService.get('generative-ai/image-generation')
            .subscribe({
                next: (result) =>
                {

                },
                error: (error) =>
                {

                },
                complete: () =>
                {

                }
            });
    }
}
