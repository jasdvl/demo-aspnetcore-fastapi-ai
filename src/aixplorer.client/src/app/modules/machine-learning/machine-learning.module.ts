import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MachineLearningRoutingModule } from './machine-learning-routing.module';
import { RegressionComponent } from './regression/regression.component';

@NgModule({
    declarations: [RegressionComponent],
    imports: [CommonModule, MachineLearningRoutingModule]
})
export class MachineLearningModule { }
