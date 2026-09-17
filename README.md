BÁO CÁO THỰC HÀNH
Môn học: Lập trình Ứng dụng .NET Core (Mã môn: 229162)

Buổi thực hành: Buổi 1 - Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)

🏗️ 1. Mô hình Kiến trúc Hệ thống
Dự án áp dụng mô hình Client - Server phân tầng rõ rệt:

Backend (MiniSupermarket.API): Đóng vai trò máy chủ xử lý logic nghiệp vụ, cung cấp các RESTful API và quản lý dữ liệu (tạm thời sử dụng In-Memory).

Frontend (MiniSupermarket.WinForms): Đóng vai trò máy trạm (Client) cung cấp giao diện người dùng (UI), tương tác với Backend thông qua HttpClient để thao tác dữ liệu theo thời gian thực.

🛠️ 2. Công nghệ Sử dụng
Ngôn ngữ nền tảng: C# trên nền .NET 8.0

Phía Server (Backend): ASP.NET Core Web API, Controllers, LINQ.

Phía Client (Frontend): Windows Forms (.NET 8.0), thư viện System.Net.Http.Json hỗ trợ gọi API và giải tuần tự hóa JSON.

Công cụ hỗ trợ & Kiểm thử: Swagger UI (Swashbuckle.AspNetCore).

📂 3. Cấu trúc Solution
Hệ thống được chia thành 2 project đồng bộ:

Plaintext
MiniSupermarketSystem/
│
├── MiniSupermarket.API/          # Backend
│   ├── Controllers/              # CategoriesController, RolesController
│   ├── Models/                   # Category.cs, Role.cs
│   └── Program.cs                # Cấu hình Swagger và Middleware
│
└── MiniSupermarket.WinForms/     # Frontend
    ├── FormCategoryManagement.cs # Giao diện CRUD Danh mục
    └── FormRoleManagement.cs     # Giao diện CRUD Vai trò
🚀 4. Kết quả Thực hiện & Hướng dẫn Kiểm thử
Kết quả đạt được:

Đã xây dựng thành công toàn bộ Backend với 2 Controller (Categories và Roles) đáp ứng đầy đủ các phương thức HTTP cơ bản: GET, POST, PUT, DELETE và chức năng Tìm kiếm (Search qua Query String).

Tích hợp thành công cấu hình Swagger UI để kiểm thử độc lập API.

Xây dựng hoàn chỉnh 2 giao diện quản lý trên WinForms, sử dụng cơ chế xử lý bất đồng bộ (async/await) với HttpClient để truyền tải dữ liệu JSON lên DataGridView mượt mà, không giật lag.

Hướng dẫn chạy dự án:

Khởi chạy MiniSupermarket.API (Set as Startup Project) trước. Kiểm tra các endpoint trên trình duyệt thông qua Swagger UI.

Kiểm tra cổng (Port) sinh ra tự động (ví dụ: https://localhost:7123) và cập nhật vào thuộc tính BaseAddress của biến _client trong project WinForms.

Khởi chạy tiếp MiniSupermarket.WinForms (Start new instance) để tiến hành nghiệm thu các thao tác Thêm, Sửa, Xóa, Tìm kiếm trực tiếp trên giao diện phần mềm.

👨‍💻 5. Tác giả
Họ tên sinh viên: Nguyễn Thị Thu Hằng

Mã sinh viên: 2124110115

Lớp học phần: CCQ2411D
