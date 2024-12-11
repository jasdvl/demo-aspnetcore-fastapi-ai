import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { of } from 'rxjs';
import { AuthService } from '../core/services/auth.service';
import { LocationService } from '../core/services/location.service';
import { MessagingSignalRService } from '../core/services/messaging-signalr.service';

// Mock für TranslateLoader
class MockTranslateLoader implements TranslateLoader
{
    getTranslation(lang: string)
    {
        return of({}); // Leeres Objekt für Testzwecke
    }
}

// Mock-Dienste erstellen
const mockAuthService = { /* Mock-Methoden für AuthService */ };
const mockLocationService = { /* Mock-Methoden für LocationService */ };
const mockSignalRService = { /* Mock-Methoden für MessagingSignalRService */ };

describe('LayoutComponent', () =>
{
    let component: LayoutComponent;
    let fixture: ComponentFixture<LayoutComponent>;

    beforeEach(async () =>
    {
        await TestBed.configureTestingModule({
            declarations: [LayoutComponent],
            imports: [
                RouterModule.forRoot([]),
                TranslateModule.forRoot({
                    loader: {
                        provide: TranslateLoader,
                        useClass: MockTranslateLoader
                    }
                })
            ],
            providers: [
                provideHttpClient(withInterceptorsFromDi()), // Verwende diesen Provider für Produktionsumgebungen
                provideHttpClientTesting(), // Verwende diesen Provider für Testumgebungen
                { provide: AuthService, useValue: mockAuthService },
                { provide: LocationService, useValue: mockLocationService },
                { provide: MessagingSignalRService, useValue: mockSignalRService }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(LayoutComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () =>
    {
        expect(component).toBeTruthy();
    });
});
