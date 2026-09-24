# BÁO CÁO THỰC HÀNH

Môn học: Lập trình Ứng dụng   
Mã môn: 229162  

Buổi thực hành: Buổi 2 - Bảo mật và phân quyền JWT cho Web API

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống

Dự án tiếp tục áp dụng mô hình **Client - Server**, trong đó bổ sung cơ chế xác thực và phân quyền bằng **JWT (JSON Web Token)**.

### Backend - MiniSupermarket.API

Đóng vai trò máy chủ, chịu trách nhiệm:

- Cung cấp RESTful Web API.
- Xử lý đăng nhập và cấp phát JWT Token.
- Xác thực người dùng bằng JWT Bearer Authentication.
- Phân quyền người dùng theo Role.
- Bảo vệ các API bằng `[Authorize]`.
- Quản lý dữ liệu danh mục sản phẩm bằng In-Memory.
- Cung cấp Swagger UI để kiểm thử API.

### Frontend - MiniSupermarket.WinForms

Đóng vai trò Client, chịu trách nhiệm:

- Cung cấp giao diện đăng nhập.
- Gửi thông tin tài khoản đến Web API.
- Nhận và lưu JWT Token sau khi đăng nhập thành công.
- Lưu Role của người dùng.
- Gửi JWT Token trong HTTP Header khi gọi API.
- Thực hiện các chức năng quản lý danh mục sản phẩm.

---

## 🛠️ 2. Công nghệ sử dụng

### Ngôn ngữ và nền tảng

- C#
- .NET 8.0

### Phía Server - Backend

- ASP.NET Core Web API
- JWT Authentication
- JWT Bearer Authentication
- Authorization / Role-based Authorization
- Controllers
- LINQ
- Data Annotations
- Swagger / OpenAPI

### Phía Client - Frontend

- Windows Forms (.NET 8.0)
- `HttpClient`
- `System.Net.Http.Json`
- `System.Net.Http.Headers`
- `System.Text.Json`
- `async/await`

### Bảo mật

- JSON Web Token (JWT)
- Bearer Authentication
- Role-based Authorization
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `System.IdentityModel.Tokens.Jwt`

---

## 📂 3. Cấu trúc Solution

```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/
│   │
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── CategoriesController.cs
│   │
│   ├── Models/
│   │   └── Category.cs
│   │
│   └── Program.cs
│
└── MiniSupermarket.WinForms/
    │
    ├── FormLogin.cs
    ├── FormCategoryManagement.cs
    ├── SessionManager.cs
    └── Program.cs
4. Tác giả
Họ và tên: Nguyễn Thị Thu Hằng
Mã sinh viên: 2124110115
Lớp học phần: CCQ2411D
