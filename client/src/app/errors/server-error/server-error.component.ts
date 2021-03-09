import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
  error: any;
  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.error;

    //We're going to safe here because we don't know if we're going to have any of this information, because what's going to happen
    // as soon as the user refreshes their page, then we immediately lose whatever's inside this navigation state
    // We only get it once when the route is activated, when we redirect the user to this server error page. So we
    // are going to be safe and we are going to use these question marks, which are referred to as optional chaning operator
  }

  ngOnInit(): void {
  }

}
