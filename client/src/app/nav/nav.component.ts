import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {} // Đây là đoạn xác thực 2 chiều sau này sẽ dùng


  constructor(public accountService: AccountService, public router: Router,
    private toast: ToastrService) { }

  ngOnInit(): void {

  }

  // login() {
  //   this.accountService.login(this.model).subscribe(Response => {
  //     console.log(Response);
  //   }, error => {
  //     console.log(error);
  //   })
  // }  //Đây là code cũ

<<<<<<< HEAD
  // login() {  // Hàm login xử lý thông báo toast
  //   this.accountService.login(this.model).subscribe(Response => {
  //     this.router.navigateByUrl('/members');
  //   }, error => {
  //     console.log(error);
  //     this.toast.error(error.error);    
  //   })
  // }

  // Bởi vì đã có xử lý lỗi xác thực get400ValidationError nên xử lý lỗi toast.error có thể bỏ đi
  // Hàm login mới

  login() {
    this.accountService.login(this.model).subscribe(Response => {
      this.router.navigateByUrl('/members');
=======
  login() {
    this.accountService.login(this.model).subscribe(Response => {
      this.router.navigateByUrl('/members');
    }, error => {
      console.log(error);
      this.toast.error(error.error);    
>>>>>>> d6ef3d549ac4e3a98dda092bb50454d4a19ca4bb
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

}
