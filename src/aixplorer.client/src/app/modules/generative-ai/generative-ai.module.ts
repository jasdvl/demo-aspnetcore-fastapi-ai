import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { GenerativeAiRoutingModule } from './generative-ai-routing.module';
import { FormsModule } from '@angular/forms'; 
import { ImageGenerationComponent } from './components/image-generation/image-generation.component';

@NgModule({
    declarations: [ImageGenerationComponent],
    imports: [CommonModule, FormsModule, GenerativeAiRoutingModule]
})
export class GenerativeAiModule { }
