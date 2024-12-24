import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImageGenerationComponent } from './image-generation/image-generation.component';

const routes: Routes = [
    {
        path: '',
        children: [
            { path: 'image-generation', component: ImageGenerationComponent }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GenerativeAiRoutingModule { }
