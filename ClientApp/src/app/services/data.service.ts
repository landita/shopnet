import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private url:string, public http:HttpClient) { }

  getAll(){
    return this.http.get(this.url);
  }

  retrieve(id){
    return this.http.get(this.url + '/' + id);
  }

  add(model){
    return this.http.post(this.url, model);
  }

  update(id, model){
    return this.http.put(this.url + '/' + id, model);
  }

  delete(id){
    return this.http.delete(this.url + '/' + id);
  }
}
