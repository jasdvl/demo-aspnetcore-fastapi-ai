import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ComputerVisionRoutingModule } from './computer-vision-routing.module';
import { ImageRecognitionComponent } from './image-recognition/image-recognition.component';

@NgModule({
    declarations: [ImageRecognitionComponent],
    imports: [CommonModule, ComputerVisionRoutingModule]
})
export class ComputerVisionModule { }
