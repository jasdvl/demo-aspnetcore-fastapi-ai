import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-image-recognition',
    templateUrl: './image-recognition.component.html',
    styleUrls: ['./image-recognition.component.css'],
})
export class ImageRecognitionComponent implements OnInit
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
