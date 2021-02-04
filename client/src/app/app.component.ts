import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit { // Cái này bấm implement thì nó sẽ ra hàm ngOnInit(): void {
  // AppComponent sẽ trở thành bootstrap khi chúng ta load ứng dụng này
  title = 'The Dating App';

  user: any; // Biến này chỉ ra rằng nó có thể là bất cứ kiểu dữ liệu nào (number,string,image)
 
  constructor(private accountService: AccountService){}
    
  ngOnInit() {   // Khi AppComponent đc khởi tạo thì hàm này sẽ đc gọi đến  
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user: User =JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }  
}
