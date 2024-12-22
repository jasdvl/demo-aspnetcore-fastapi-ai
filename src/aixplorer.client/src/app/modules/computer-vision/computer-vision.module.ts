import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ComputerVisionRoutingModule } from './computer-vision-routing.module';
import { ImageInterpretationComponent } from './components/image-interpretation/image-interpretation';

@NgModule({
    declarations: [ImageInterpretationComponent],
    imports: [CommonModule, ComputerVisionRoutingModule]
})
export class ComputerVisionModule { }
