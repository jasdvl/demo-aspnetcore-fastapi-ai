import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'app/core/services/rest-api.service';
import { ImageInterpretationResultDto } from '../../interfaces/image-interpretation-result.dto';

@Component({
    selector: 'app-image-interpretation',
    templateUrl: './image-interpretation.component.html',
    styleUrls: ['./image-interpretation.component.css'],
    standalone: false
})
export class ImageInterpretationComponent implements OnInit
{
    selectedFile: File | null = null;

    imageEnc: string | null = null;

    user_question: string | null = null;

    interpretation: string | null = null;

    // True if the AI is processing a response
    isAwaitingResponse: boolean = false;

    constructor(private router: Router, private apiService: ApiService)
    {
    }

    ngOnInit(): void
    {
        
    }

    ngOnDestroy()
    {
    }

    startRecognition()
    {
        if (!this.selectedFile)
        {
            return;
        }

        const reader = new FileReader();
        reader.readAsDataURL(this.selectedFile);
        reader.onload = () =>
        {
            const base64String = reader.result as string;

            // Remove prefix e.g. "data:image/jpeg;base64,"
            const base64Image = base64String.split(',')[1];

            const payload = {
                image_base64: base64Image,
                filename: this.selectedFile!.name,
                user_question: this.user_question
            };

            this.interpretation = null;
            this.isAwaitingResponse = true;

            this.apiService.post<ImageInterpretationResultDto>(
                                                        "computer-vision/image-interpretation",
                                                        payload
            ).subscribe({
                next: (result) =>
                {
                    this.interpretation = result.description;
                },
                error: (error) =>
                {
                    // Log to console, use a logger service or forward the error to monitoring tools like Sentry
                },
                complete: () =>
                {
                    this.isAwaitingResponse = false;
                }
            });
        };
    }

    onFileSelected(event: Event): void
    {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files.length > 0)
        {
            this.selectedFile = input.files[0];

            // Create image preview
            const reader = new FileReader();
            reader.onload = () =>
            {
                this.imageEnc = reader.result as string;
            };
            reader.readAsDataURL(this.selectedFile);

            this.user_question = "";
        }
    }
}
