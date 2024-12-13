import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'app/core/services/rest-api.service';

@Component({
    selector: 'app-image-recognition',
    templateUrl: './image-recognition.component.html',
    styleUrls: ['./image-recognition.component.css'],
})
export class ImageRecognitionComponent implements OnInit
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

    startRecognition()
    {
        if (!this.selectedFile)
        {
            return;
        }

        const formData = new FormData();
        formData.append('file', this.selectedFile, this.selectedFile.name);

        this.apiService.post("computer-vision/image-recognition", formData, new HttpHeaders())
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

    onFileSelected(event: Event): void
    {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files.length > 0)
        {
            this.selectedFile = input.files[0];
        }
    }
}
