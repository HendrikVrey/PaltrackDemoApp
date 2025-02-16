import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import type { PersonI, UserI } from '../models/app.models';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(private http: HttpClient) {
  }

  //#region API Calls
  getPersons(): Observable<PersonI[]> {
    return this.http.get<PersonI[]>('http://localhost:5255/api/persons')
  }

  createPerson(person: PersonI): Observable<PersonI> {
    return this.http.post<PersonI>('http://localhost:5255/api/persons', person);
  }

  updatePerson(id: number, person: PersonI): Observable<any> {
    return this.http.put(`http://localhost:5255/api/persons/${id}`, person);
  }

  deletePerson(id: number): Observable<any> {
    return this.http.delete(`http://localhost:5255/api/persons/${id}`);
  }
  //#endregion
}
