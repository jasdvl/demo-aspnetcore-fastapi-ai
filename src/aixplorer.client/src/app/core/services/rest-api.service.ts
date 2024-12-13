import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ApiService
{
    private apiUrl = `${environment.apiUrl}api`;

    // Standard-Header für JSON-Anfragen
    private headers = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(private http: HttpClient) { }

    
    get<T>(endpoint: string): Observable<T>
    {
        return this.http.get<T>(`${this.apiUrl}/${endpoint}`, { headers: this.headers });
    }

    post<T>(endpoint: string, body: any, headers?: HttpHeaders): Observable<T>
    {
        // Default header if no header set
        const effectiveHeaders = headers ?? this.headers;
        return this.http.post<T>(`${this.apiUrl}/${endpoint}`, body, { headers: effectiveHeaders });
    }

    put<T>(endpoint: string, body: any): Observable<T>
    {
        return this.http.put<T>(`${this.apiUrl}/${endpoint}`, body, { headers: this.headers });
    }

    patch<T>(endpoint: string, body: any): Observable<T>
    {
        return this.http.patch<T>(`${this.apiUrl}/${endpoint}`, body, { headers: this.headers });
    }

    delete<T>(endpoint: string): Observable<T>
    {
        return this.http.delete<T>(`${this.apiUrl}/${endpoint}`, { headers: this.headers });
    }
}
