import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImageRecognitionComponent } from './image-recognition/image-recognition.component';

const routes: Routes = [
    //{
    //    path: 'image-recognition', component: ImageRecognitionComponent
    //}
    {
        path: '',
        children: [
            { path: 'image-recognition', component: ImageRecognitionComponent }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComputerVisionRoutingModule { }
