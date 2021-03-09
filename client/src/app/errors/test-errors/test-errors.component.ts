import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseurl = "https://localhost:5001/API/";
  validationErrors: string[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error() {
    this.http.get(this.baseurl + 'Buggy/not-found').subscribe(Response => {
      // Nó sẽ chạy đến BuggyController phần  [HttpGet("not-found")]
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }

  get400Error() {
    this.http.get(this.baseurl + 'Buggy/bad-request').subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }

  get500Error() {
    this.http.get(this.baseurl + 'Buggy/server-error').subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }

  get401Error() {
    this.http.get(this.baseurl + 'Buggy/auth').subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    })
  }
 
  get400ValidationError() {
    this.http.post(this.baseurl + 'Account/register',{}).subscribe(Response => {
      console.log(Response);
    }, error => {
      console.log(error);
      this.validationErrors = error;
    })
  }

}
