# BÁO CÁO THỰC HÀNH

**Môn học:** Lập trình Ứng dụng  
**Mã môn:** 229162  
**Buổi thực hành:** Buổi 4 – Thiết kế kiến trúc giao diện WinForms Shell và điều hướng phân quyền Admin / Cashier / Warehouse

## 🏗️ 1. Mô hình Kiến trúc Hệ thống

Dự án tiếp tục áp dụng mô hình **Client – Server**, sử dụng ASP.NET Core Web API làm Backend và Windows Forms làm Frontend. Dữ liệu được lưu trữ trong Microsoft SQL Server thông qua Entity Framework Core.

Trong Buổi 4, giao diện được tổ chức theo mô hình **WinForms Shell**, sử dụng một Form chính chứa thanh điều hướng Sidebar và vùng hiển thị các Form chức năng.

### Backend – MiniSupermarket.API

Đóng vai trò máy chủ trung tâm, chịu trách nhiệm:

- Cung cấp RESTful Web API phục vụ quản lý danh mục, sản phẩm và khách hàng.
- Kết nối và thao tác với SQL Server thông qua Entity Framework Core.
- Tiếp nhận yêu cầu từ ứng dụng WinForms và trả về dữ liệu JSON.
- Xử lý các thao tác CRUD bằng LINQ và lập trình bất đồng bộ `async/await`.
- Cung cấp Swagger UI để kiểm thử các API.

### Frontend – MiniSupermarket.WinForms

Đóng vai trò Client giao tiếp với người dùng, chịu trách nhiệm:

- Sử dụng `FormMainShell` làm giao diện điều khiển trung tâm.
- Hiển thị các nút chức năng trên Sidebar theo vai trò người dùng.
- Nhúng các Form con vào `panelMainContent`.
- Điều hướng giữa các màn hình mà không mở nhiều cửa sổ độc lập.
- Gọi Backend thông qua `HttpClient` và xử lý dữ liệu JSON.
- Hiển thị dữ liệu lên `DataGridView`, tiếp nhận thông tin nhập và thông báo kết quả xử lý.

### Phân quyền giao diện

| Vai trò | Chức năng được phép sử dụng |
|---|---|
| **Admin** | Danh mục, sản phẩm, khách hàng, POS, báo cáo doanh thu và quản trị tài khoản. |
| **Cashier** | POS và quản lý khách hàng. |
| **Warehouse** | Quản lý danh mục và sản phẩm. |

Khi người dùng đăng nhập, hệ thống xác định vai trò để hiển thị các nút phù hợp và mở màn hình mặc định tương ứng.

## 🛠️ 2. Công nghệ sử dụng

### Ngôn ngữ và nền tảng

- C#.
- .NET 8.0.

### Phía Server – Backend

- ASP.NET Core Web API.
- Entity Framework Core.
- EF Core Code-First Migrations.
- Dependency Injection.
- LINQ và lập trình bất đồng bộ `async/await`.
- Swagger / OpenAPI.

### Cơ sở dữ liệu

- Microsoft SQL Server.
- SQL Server Management Studio – SSMS.

### Phía Client – Frontend

- Windows Forms (.NET 8.0).
- `HttpClient`.
- `System.Net.Http.Json`.
- Các thành phần giao diện: `Panel`, `Button`, `Label`, `TextBox`, `ComboBox`, `NumericUpDown` và `DataGridView`.
- Thuộc tính `Dock` để bố trí Sidebar và vùng nội dung.
- Thuộc tính `TopLevel`, `FormBorderStyle` và `Dock` để nhúng Form con.
- Sự kiện `Click`, `Load` và `CellClick` để xử lý tương tác.

## 📂 3. Cấu trúc Solution

Các thành phần chính đã triển khai hoặc đang hoàn thiện trong Buổi 4:

| Dự án / Thư mục | File | Chức năng |
|---|---|---|
| **MiniSupermarket.API / Controllers** | `CategoriesController.cs` | API quản lý danh mục. |
| | `ProductsController.cs` | API quản lý và tra cứu sản phẩm. |
| | `CustomersController.cs` | API quản lý khách hàng. |
| **MiniSupermarket.API / Models** | `Category.cs` | Mô hình danh mục. |
| | `Product.cs` | Mô hình sản phẩm. |
| | `Customer.cs` | Mô hình khách hàng. |
| **MiniSupermarket.API / Data** | `SupermarketDbContext.cs` | Kết nối và ánh xạ dữ liệu bằng EF Core. |
| **MiniSupermarket.API / Migrations** | Các file Migration | Quản lý thay đổi cấu trúc cơ sở dữ liệu. |
| **MiniSupermarket.API** | `appsettings.json`, `Program.cs` | Cấu hình kết nối và khởi tạo Backend. |
| **MiniSupermarket.WinForms** | `FormMainShell.cs`, `FormMainShell.Designer.cs` | Giao diện chính, điều hướng và phân quyền. |
| | `FormCategoryManagement.cs` | Quản lý danh mục. |
| | `FormProductManagement.cs`, `FormProductManagement.Designer.cs` | Quản lý sản phẩm và tồn kho. |
| | `FormCustomerManagement.cs`, `FormCustomerManagement.Designer.cs` | Quản lý khách hàng. |
| | `FormPOS.cs`, `FormPOS.Designer.cs` | Giao diện bán hàng tại quầy. |
| | `FormQuickReport.cs`, `FormQuickReport.Designer.cs` | Phần báo cáo doanh thu đang hoàn thiện. |
| | `FormUserManagement.cs`, `FormUserManagement.Designer.cs` | Phần quản trị tài khoản đang hoàn thiện. |
| | `Program.cs` | Điểm khởi chạy ứng dụng WinForms. |

## 👨‍💻 4. Tác giả

**Họ và tên:** Nguyễn Thị Thu Hằng  
**Mã sinh viên:** 2124110115  
**Lớp học phần:** CCQ2411D
