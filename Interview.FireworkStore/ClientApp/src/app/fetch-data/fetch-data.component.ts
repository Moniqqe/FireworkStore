import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public fireworks: Firework[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Firework[]>(baseUrl + 'firework').subscribe(result => {
      this.fireworks = result;
    }, error => console.error(error));
  }
}

interface Firework {
  
}
