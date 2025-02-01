import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NlpRoutingModule } from './nlp-routing.module';
import { QuestionAnsweringComponent } from './components/question-answering/question-answering.component';
import { MarkdownModule, MarkdownPipe } from 'ngx-markdown';

@NgModule({
    declarations: [QuestionAnsweringComponent],
    imports: [CommonModule, FormsModule, NlpRoutingModule, MarkdownModule.forRoot()],
    providers: [MarkdownPipe]
})
export class NlpModule { }
