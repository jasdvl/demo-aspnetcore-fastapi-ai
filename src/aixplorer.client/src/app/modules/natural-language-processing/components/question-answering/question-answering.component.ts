import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'app/core/services/rest-api.service';
import { QuestionAnswerResultDto } from '../../interfaces/question-answering-result.dto';
import { MarkdownPipe } from 'ngx-markdown';

@Component({
    selector: 'app-question-answering',
    templateUrl: './question-answering.component.html',
    styleUrls: ['./question-answering.component.css'],
    standalone: false
})
export class QuestionAnsweringComponent implements OnInit
{
    userInput: string = '';

    chatHistory: { question: string, answer: string | null }[] = [];

    // True if the AI is processing a response
    isAwaitingResponse: boolean = false;

    constructor(private router: Router, private apiService: ApiService, private markdownPipe: MarkdownPipe)
    {
    }

    ngOnInit(): void
    {
    }

    ngOnDestroy()
    {
    }

    submitMessage()
    {
        if (!this.userInput.trim())
        {
            return;
        }

        this.chatHistory.push({ question: this.userInput, answer: null });

        const payload = { question: this.userInput };
        
        const lastIndex = this.chatHistory.length - 1;
        this.isAwaitingResponse = true;

        this.apiService.post<QuestionAnswerResultDto>(
            "nlp/question-answering",
            payload
        ).subscribe({
            next: (result) =>
            {
                const lastIndex = this.chatHistory.length - 1;
                this.chatHistory[lastIndex].answer = result.answer.slice(1, -1);
            },
            error: (error) =>
            {
                // Log to console, use a logger service or forward the error to monitoring tools like Sentry
                console.error('Error:', error);
                const lastIndex = this.chatHistory.length - 1;
                this.chatHistory[lastIndex].answer = 'An error occurred. Please try again.';
            },
            complete: () =>
            {
                this.isAwaitingResponse = false;
            }
        });

        this.userInput = '';
    }
}
