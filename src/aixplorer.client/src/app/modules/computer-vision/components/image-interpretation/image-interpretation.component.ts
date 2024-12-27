import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'app/core/services/rest-api.service';
import { ImageInterpretationResultDto } from '../../interfaces/image-interpretation-result.dto';

@Component({
    selector: 'app-image-interpretation',
    templateUrl: './image-interpretation.component.html',
    styleUrls: ['./image-interpretation.component.css'],
})
export class ImageInterpretationComponent implements OnInit
{
    selectedFile: File | null = null;

    constructor(private router: Router, private apiService: ApiService)
    {
    }

    ngOnInit(): void
    {
        
    }

    ngOnDestroy()
    {
    }

    postFormData()
    {
        if (!this.selectedFile)
        {
            return;
        }

        const formData = new FormData();
        formData.append('file', this.selectedFile, this.selectedFile.name);

        this.apiService.post<ImageInterpretationResultDto>("computer-vision/image-interpretation", formData, new HttpHeaders())
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
                filename: this.selectedFile!.name
            };

            this.apiService.post<ImageInterpretationResultDto>(
                                                        "computer-vision/image-interpretation",
                                                        payload
            ).subscribe({
                next: (result) =>
                {
                    // Verarbeiten Sie das Ergebnis hier
                },
                error: (error) =>
                {
                    // Fehlerbehandlung hier
                },
                complete: () =>
                {
                    // Abschlusslogik hier
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
        }
    }
}
