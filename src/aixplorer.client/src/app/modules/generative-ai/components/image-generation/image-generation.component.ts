import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'app/core/services/rest-api.service';
import { GeneratedImageDto } from '../../interfaces/generated-image.dto';

@Component({
    selector: 'app-image-generation',
    templateUrl: './image-generation.component.html',
    styleUrls: ['./image-generation.component.css'],
})
export class ImageGenerationComponent implements OnInit
{
    textInputValue: string = '';

    imageEnc: string | null = null;

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
        const requestBody = { prompt: this.textInputValue };

        this.apiService.post<GeneratedImageDto>('generative-ai/image-generation', requestBody)
            .subscribe({
                next: (result) =>
                {
                    this.imageEnc = result.base64_image;
                },
                error: (error) =>
                {
                    // Log to console, use a logger service or forward the error to monitoring tools like Sentry
                    console.error('Error during image generation:', error);
                }
            });
    }
}
