import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegressionComponent } from './regression/regression.component';

const routes: Routes = [
    {
        path: 'regression', component: RegressionComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MachineLearningRoutingModule { }
