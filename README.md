BÁO CÁO THỰC HÀNH
Môn học: Lập trình Ứng dụng

Mã môn: 229162

Buổi thực hành: Buổi 3 - Tích hợp SQL Server và Entity Framework Core Code-First & Quản lý Khách hàng

🏗️️ 1. Mô hình Kiến trúc Hệ thống
Dự án tiếp tục áp dụng mô hình Client - Server, nâng cấp cơ chế lưu trữ từ bộ nhớ tạm (In-Memory) sang hệ quản trị cơ sở dữ liệu quan hệ Microsoft SQL Server.

Backend - MiniSupermarket.API
Đóng vai trò máy chủ trung tâm, chịu trách nhiệm:

Cung cấp RESTful Web API cho các phân hệ Danh mục (Categories), Sản phẩm (Products) và Khách hàng (Customers).

Kết nối và tương tác với SQL Server thông qua ORM Entity Framework Core.

Quản lý cấu trúc cơ sở dữ liệu bằng cơ chế Code-First Migrations.

Tự động nạp dữ liệu mồi (Data Seeding) cho hệ thống.

Xử lý các truy vấn CRUD bất đồng bộ (async/await) kết hợp LINQ.

Cung cấp Swagger UI để kiểm thử API trực tiếp.

Frontend - MiniSupermarket.WinForms
Đóng vai trò Client giao tiếp với người dùng, chịu trách nhiệm:

Cung cấp giao diện trực quan quản lý danh mục và thông tin khách hàng (FormCustomerManagement).

Gọi các API Backend thông qua HttpClient (hỗ trợ GetFromJsonAsync, PostAsJsonAsync, PutAsJsonAsync, DeleteAsync).

Xử lý phản hồi từ server, ánh xạ dữ liệu lên DataGridView và cập nhật UI.

🛠️ 2. Công nghệ sử dụng
Ngôn ngữ và nền tảng
C#

.NET 8.0

Phía Server - Backend
ASP.NET Core Web API

Entity Framework Core (Microsoft.EntityFrameworkCore.SqlServer, Tools, Design)

EF Core Migrations (Add-Migration, Update-Database)

Data Annotations (Validation & Schema Mapping)

Dependency Injection (DI) cho DbContext

LINQ & Asynchronous Programming (async/await, Task)

Swagger / OpenAPI

Cơ sở dữ liệu
Microsoft SQL Server

SQL Server Management Studio (SSMS)

Phía Client - Frontend
Windows Forms (.NET 8.0)

HttpClient

System.Net.Http.Json

📂 3. Cấu trúc Solution
Plaintext
MiniSupermarketSystem/
│
├── MiniSupermarket.API/
│   │
│   ├── Controllers/
│   │   ├── CategoriesController.cs
│   │   └── CustomersController.cs
│   │
│   ├── Models/
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   └── Customer.cs
│   │
│   ├── Data/
│   │   └── SupermarketDbContext.cs
│   │
│   ├── Migrations/
│   │   └── (Chứa các file sinh tự động từ Add-Migration)
│   │
│   ├── appsettings.json
│   └── Program.cs
│
└── MiniSupermarket.WinForms/
    │
    ├── FormCategoryManagement.cs
    ├── FormCustomerManagement.cs
    ├── FormCustomerManagement.Designer.cs
    └── Program.cs
👨‍💻 4. Tác giả
Họ và tên: Nguyễn Thị Thu Hằng

Mã sinh viên: 2124110115

Lớp học phần: CCQ2411D
