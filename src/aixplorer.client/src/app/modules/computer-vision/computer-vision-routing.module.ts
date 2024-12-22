import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImageInterpretationComponent } from './components/image-interpretation/image-interpretation';

const routes: Routes = [
    //{
    //    path: 'image-interpretation', component: ImageInterpretationComponent
    //}
    {
        path: '',
        children: [
            { path: 'image-interpretation', component: ImageInterpretationComponent }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComputerVisionRoutingModule { }
