import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit { // Cái này bấm implement thì nó sẽ ra hàm ngOnInit(): void {
  // AppComponent sẽ trở thành bootstrap khi chúng ta load ứng dụng này
  title = 'The Dating App';

  user: any; // Biến này chỉ ra rằng nó có thể là bất cứ kiểu dữ liệu nào (number,string,image)
 
  constructor(private http:HttpClient){}
    
  ngOnInit() {
    this.getUsers() // Khi AppComponent đc khởi tạo thì hàm này sẽ đc gọi đến
  }

  getUsers()  {
    this.http.get('https://localhost:5001/API/user/').subscribe(  Response=>{
      // localhost của API là 5001 mà của Angular là 4200 nên reponse của Angular bị trình duyệt chặn lại
      // Nó không đc phép lấy tài nguyên từ cổng khác
      this.user = Response;
    }, error =>{
      console.log(error); // Log out với bất kỳ lỗi nào
    }
    ) 
    
    // Cái phản hồi này (this response) sẽ chứa User từ API, Chúng ta có thể Set thuộc tính User ở đây
    //cho cái phản hồi nhận được từ máy chủ API của mình (Cái user: any khai báo ở trên)
     
    // Http đc dùng để tạo ra 1 request
    // Hàm subscribe sẽ phản hồi bất cứ thứ thứ gì nhưng 1 phản hồi 200 OK, nó sẽ chuyển đến phần Error của phần parameter
    // @return - Một phản hồi có thể quan sát được, với phần thân phản hồi là Bộ đệm mảng.
    // với phần nội dung phản hồi, user của chúng tôi sẽ nằm trong phần nội dung phản hồi trong API server
  }

  
}
