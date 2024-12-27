import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuestionAnsweringComponent } from './components/question-answering/question-answering.component';

const routes: Routes = [
    //{
    //    path: 'question-answering', component: QuestionAnsweringComponent
    //}
    {
        path: '',
        children: [
            { path: 'question-answering', component: QuestionAnsweringComponent }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NlpRoutingModule { }
