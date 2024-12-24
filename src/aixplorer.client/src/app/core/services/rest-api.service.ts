import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ApiService
{
    private proxyUrl = `${environment.proxyUrl}api`;

    // Standard-Header für JSON-Anfragen
    private jsonHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(private http: HttpClient) { }

    
    get<T>(endpoint: string): Observable<T>
    {
        return this.http.get<T>(`${this.proxyUrl}/${endpoint}`, { headers: this.jsonHeaders });
    }

    post<T>(endpoint: string, body: any, headers?: HttpHeaders): Observable<T>
    {
        // application/json headers if no headers set
        const effectiveHeaders = headers ?? this.jsonHeaders;
        return this.http.post<T>(`${this.proxyUrl}/${endpoint}`, body, { headers: effectiveHeaders });
    }

    put<T>(endpoint: string, body: any): Observable<T>
    {
        return this.http.put<T>(`${this.proxyUrl}/${endpoint}`, body, { headers: this.jsonHeaders });
    }

    patch<T>(endpoint: string, body: any): Observable<T>
    {
        return this.http.patch<T>(`${this.proxyUrl}/${endpoint}`, body, { headers: this.jsonHeaders });
    }

    delete<T>(endpoint: string): Observable<T>
    {
        return this.http.delete<T>(`${this.proxyUrl}/${endpoint}`, { headers: this.jsonHeaders });
    }
}
