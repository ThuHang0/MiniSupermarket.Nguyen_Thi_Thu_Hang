# BÁO CÁO THỰC HÀNH

**Môn học:** Lập trình Ứng dụng  
**Mã môn:** 229162  
**Buổi thực hành:** Buổi 6 – Entity Framework Core nâng cao, phân trang LINQ và tích hợp hệ thống POS Client – Server

## 🏗️ 1. Mô hình Kiến trúc Hệ thống

Dự án tiếp tục áp dụng mô hình **Client – Server**, sử dụng ASP.NET Core Web API, Windows Forms và Microsoft SQL Server. Buổi 6 tập trung phân trang tại Backend, truy vấn dữ liệu quan hệ bằng EF Core và kết nối màn hình quản lý sản phẩm WinForms với API có xác thực JWT.

### Backend – MiniSupermarket.API

Đóng vai trò máy chủ trung tâm, chịu trách nhiệm:

- Bổ sung API `GET /api/products/paged`, hỗ trợ từ khóa, danh mục, số trang và kích thước trang.
- Sử dụng LINQ `OrderBy`, `Skip`, `Take` trước `ToListAsync` để phân trang tại SQL Server.
- Kết hợp `Include`, `AsNoTracking` và ánh xạ DTO để trả sản phẩm kèm tên danh mục.
- Đếm số dòng sau khi lọc, trả metadata gồm `CurrentPage`, `PageSize`, `TotalItems`, `TotalPages` và `Items`.
- Chuẩn hóa tham số trang và đưa yêu cầu vượt số trang về trang cuối hợp lệ.
- Bổ sung API `GET /api/customers/paged`, sắp xếp điểm thưởng giảm dần và mã khách tăng dần khi bằng điểm.
- Duy trì CRUD sản phẩm; kiểm tra dữ liệu đầu vào, danh mục và barcode trùng.
- Chặn xóa sản phẩm đã có trong hóa đơn, danh mục đang có sản phẩm và khách hàng có hóa đơn; trả HTTP 409 kèm thông báo nghiệp vụ.
- Bổ sung xác thực và phân quyền cho API danh mục, khách hàng và vai trò.
- Bật log câu lệnh EF Core để quan sát truy vấn phân trang `OFFSET` / `FETCH NEXT`.

### Frontend – MiniSupermarket.WinForms

Đóng vai trò Client giao tiếp với người dùng, chịu trách nhiệm:

- Gọi API phân trang bằng `HttpClient` dùng chung và Bearer JWT.
- Ánh xạ kết quả vào `PagedViewResult<ProductDto>` và hiển thị `Items` trên `DataGridView`.
- Bổ sung nút Trang trước, Trang sau và nhãn số trang, tổng số sản phẩm.
- Khóa nút chuyển trang ở giới hạn đầu/cuối và trong lúc tải dữ liệu.
- Tìm kiếm theo tên/barcode, lọc danh mục bằng ComboBox và tải lại từ trang đầu khi thay điều kiện lọc.
- Gửi yêu cầu Thêm, Sửa, Xóa về Backend rồi tải lại dữ liệu phân trang.
- Hiển thị thông báo khi API từ chối xóa dữ liệu liên quan; sử dụng HttpClient có JWT cho form vai trò.
- Tiếp tục sử dụng giao diện Shell và các chức năng POS, khách hàng, tài khoản, báo cáo của các buổi trước.

Bài mở rộng phân trang khách hàng được triển khai ở API; màn hình khách hàng hiện vẫn dùng danh sách cũ. Vai trò hiện vẫn được lưu trong RAM theo thiết kế trước đó.

## 🛠️ 2. Công nghệ sử dụng

### Ngôn ngữ và nền tảng

- C# và .NET 8.0.
- Visual Studio 2022.

### Phía Server – Backend

- ASP.NET Core Web API.
- Entity Framework Core SQL Server, Design và Tools.
- LINQ: `Include`, `AsNoTracking`, `Where`, `Contains`, `CountAsync`, `OrderBy`, `Skip`, `Take`, `Select`.
- Generic DTO, Data Annotations, Dependency Injection và async/await.
- JWT Authentication, phân quyền theo vai trò và Swagger / OpenAPI.

### Cơ sở dữ liệu

- Microsoft SQL Server và SQL Server Management Studio (SSMS).
- Phân trang tại server bằng `OFFSET` / `FETCH NEXT`.
- Khóa ngoại bảo vệ dữ liệu liên quan.
- Tiếp tục sử dụng cấu trúc database và Stored Procedure đã triển khai trong Buổi 5; các bổ sung của Buổi 6 không yêu cầu Migration mới.

### Phía Client – Frontend

- Windows Forms (.NET 8.0).
- `HttpClient`, `System.Net.Http.Json` và Bearer JWT.
- `DataGridView`, ComboBox, Button, Label và TableLayoutPanel.
- Gọi API bất đồng bộ bằng async/await.

## 📂 3. Cấu trúc Solution

Các file chính liên quan đến Buổi 6 và các phân hệ được sử dụng; lược bỏ file mẫu, `.resx`, file build và các file Migration chi tiết:

```text
MiniSupermarket.Nguyen_Thi_Thu_Hang/
├── MiniSupermarket.Nguyen_Thi_Thu_Hang.sln
├── README.md
├── MiniSupermarket.API/
│   ├── Controllers/
│   │   ├── AdoProductController.cs
│   │   ├── AuthController.cs
│   │   ├── CategoriesController.cs
│   │   ├── CustomersController.cs
│   │   ├── OrdersController.cs
│   │   ├── ProductsController.cs
│   │   ├── ReportsController.cs
│   │   ├── RolesController.cs
│   │   └── UsersController.cs
│   ├── Models/
│   │   ├── PagedResponseDto.cs
│   │   ├── CashierRevenueDto.cs
│   │   ├── Category.cs
│   │   ├── Customer.cs
│   │   ├── Product.cs
│   │   ├── Order.cs
│   │   ├── OrderDetail.cs
│   │   └── Role.cs
│   ├── Migrations/
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── SupermarketDbContext.cs
│   ├── User.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── MiniSupermarket.API.csproj
│   └── Program.cs
└── MiniSupermarket.WinForms/
    ├── FormLogin.cs
    ├── FormLogin.Designer.cs
    ├── FormMainShell.cs
    ├── FormMainShell.Designer.cs
    ├── FormProductManagement.cs
    ├── FormProductManagement.Designer.cs
    ├── FormCategoryManagement.cs
    ├── FormCategoryManagement.Designer.cs
    ├── FormCustomerManagement.cs
    ├── FormCustomerManagement.Designer.cs
    ├── FormPOS.cs
    ├── FormPOS.Designer.cs
    ├── FormQuickReport.cs
    ├── FormQuickReport.Designer.cs
    ├── FormUserManagement.cs
    ├── FormUserManagement.Designer.cs
    ├── FormRoleManagement.cs
    ├── FormRoleManagement.Designer.cs
    ├── SessionManager.cs
    ├── MiniSupermarket.WinForms.csproj
    └── Program.cs
```

`PagedResponseDto.cs` chứa `ProductItemDto` và `PagedResult<T>` phía Backend. `ProductDto` và `PagedViewResult<T>` được khai báo trong `FormProductManagement.cs`. Lớp `ApiClientService` nằm trong `SessionManager.cs`.

## 👩‍💻 4. Tác giả

**Họ và tên:** Nguyễn Thị Thu Hằng  
**Mã sinh viên:** 2124110115  
**Lớp học phần:** CCQ2411D
