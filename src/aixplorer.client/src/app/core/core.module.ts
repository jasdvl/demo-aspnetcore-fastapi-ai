import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule, Optional, SkipSelf } from '@angular/core';

@NgModule({
    declarations: [],
    imports: [CommonModule],
    providers: [
        
    ],
})
export class CoreModule
{
    constructor(@Optional() @SkipSelf() core: CoreModule)
    {
        if (core)
        {
            throw new Error('Core Module can only be imported to AppModule.');
        }
    }
}
