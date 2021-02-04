import { HttpClient } from '@angular/common/http';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({ // Có nghĩa rằng cái service này có thể tiêm vào trong các thành phần or service khác trong ứng dụng
  providedIn: 'root'  // Metadata called provided in root
  // Khi chúng ta tiêm account service này vào các thành phần khác và nó đc khởi tạo, nó vẫn sẽ đc khởi tạo
  // Cho đến khi ứng dụng đc đóng lại
  // Khi User chuyển sang trang khác, hoặc đóng lại, hoặc chuyển qua phần mềm khác thì các account service này
  // sẽ bị hủy (destroyed). Nhưng nếu họ ở lại App thì Service này sẽ đc khới tạo suốt quá trình tồn tại của App
})
export class AccountService {
  // Chúng ta sẽ dùng service này để tạo request đến API

  baseUrl = 'https://localhost:5001/API/';
  private currentUserSource = new ReplaySubject<User>(1); // Đây là 1 loại đặc biệt của Observable
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient) { }

  login(model: any) // Login sẽ nhận đc chứng chỉ từ navbav Form login
  {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(// Trong model sẽ chứa username và password mà chúng ta muốn
      // gửi lên server
      map((Response: User) => { // Hàm này sẽ get user từ Response
        const user = Response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));  // We get back in local storage in the browser
          // When we subcribe or when we log in, then this Func
          //is going to run and going to populate our user ipnside local
          //storage in the browser
          this.currentUserSource.next(user);
        }
      }
      )
    )
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }  
      })
    )
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

}
