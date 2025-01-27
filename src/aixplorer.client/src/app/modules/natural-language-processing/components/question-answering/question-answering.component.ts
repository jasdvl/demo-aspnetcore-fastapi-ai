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

    chatHistory: { question: string, answer: string | null }[] = [];

    constructor(private router: Router, private apiService: ApiService)
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
        /*this.chatHistory[lastIndex].answer = "This is my answer.";*/

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
                console.error('Error:', error);
                const lastIndex = this.chatHistory.length - 1;
                this.chatHistory[lastIndex].answer = 'An error occurred. Please try again.';
            }
        });

        this.userInput = '';
    }

    formatAnswer(answer: string): string
    {
        if (answer.includes('```csharp'))
        {
            return answer.replace(/```csharp/g, '<pre><code class="language-csharp">')
                .replace(/```/g, '</code></pre>');
        }

        return answer;
    }
}
