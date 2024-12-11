import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule)
    },
    {
        path: 'computer-vision',
        loadChildren: () => import('./modules/computer-vision/computer-vision.module').then((m) => m.ComputerVisionModule)
    },
    {
        path: 'machine-learning',
        loadChildren: () => import('./modules/machine-learning/machine-learning.module').then((m) => m.MachineLearningModule)
    },
    { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
