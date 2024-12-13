import { CommonModule } from '@angular/common';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule, Optional, SkipSelf } from '@angular/core';

@NgModule({
    declarations: [],
    imports: [CommonModule],
    providers: [
        provideHttpClient(withInterceptorsFromDi())
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
