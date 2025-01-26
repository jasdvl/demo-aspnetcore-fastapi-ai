import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ComputerVisionRoutingModule } from './computer-vision-routing.module';
import { ImageInterpretationComponent } from './components/image-interpretation/image-interpretation.component';
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations: [ImageInterpretationComponent],
    imports: [CommonModule, FormsModule, ComputerVisionRoutingModule]
})
export class ComputerVisionModule { }
