import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'app/core/services/rest-api.service';
import { QuestionAnswerResultDto } from '../../interfaces/question-answering-result.dto';

@Component({
    selector: 'app-question-answering',
    templateUrl: './question-answering.component.html',
    styleUrls: ['./question-answering.component.css'],
})
export class QuestionAnsweringComponent implements OnInit
{
    userInput: string = '';

    constructor(private router: Router, private apiService: ApiService)
    {
    }

    ngOnInit(): void
    {
        
    }

    ngOnDestroy()
    {
    }

    startRequest()
    {
        const payload = { question: this.userInput };

        this.apiService.post<QuestionAnswerResultDto>(
                                                    "nlp/question-answering",
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
    }
}
