BÁO CÁO THỰC HÀNH
Môn học: Lập trình Ứng dụng
Mã môn: 229162
Buổi thực hành: Buổi 5 – ADO.NET nâng cao, Stored Procedure và giao dịch POS Checkout
🏗️ 1. Mô hình Kiến trúc Hệ thống
Dự án tiếp tục sử dụng mô hình Client – Server, với ASP.NET Core Web API làm Backend, WinForms làm Client và Microsoft SQL Server lưu trữ dữ liệu. Buổi 5 bổ sung ADO.NET trực tiếp bên cạnh EF Core, báo cáo bằng Stored Procedure và xử lý giao dịch thanh toán bằng SqlTransaction.
Backend – MiniSupermarket.API
Đóng vai trò máy chủ trung tâm, chịu trách nhiệm:
- Cung cấp RESTful Web API quản lý danh mục, sản phẩm, khách hàng, tài khoản và bán hàng.
- Sử dụng SqlConnection, SqlCommand, SqlParameter để kết nối và truy vấn SQL Server an toàn.
- Đếm sản phẩm bằng ExecuteScalar, cập nhật tồn kho bằng ExecuteNonQuery, đọc danh sách và tra cứu barcode bằng SqlDataReader.
- Sử dụng DataTable và SqlDataAdapter để minh họa truy xuất dữ liệu theo mô hình ngắt kết nối.
- Cung cấp API lọc sản phẩm tồn kho thấp và đếm sản phẩm theo danh mục.
- Gọi Stored Procedure để báo cáo doanh thu theo thu ngân, khoảng ngày và Top 5 sản phẩm bán chạy.
- Xử lý thanh toán trong cùng SqlTransaction: tạo hóa đơn, lưu chi tiết, trừ tồn kho và cộng điểm khách hàng.
- Commit khi thành công, Rollback khi phát sinh lỗi; hỗ trợ hủy hóa đơn, hoàn kho và thu hồi điểm.
- Lấy danh tính thu ngân từ JWT, lấy giá bán từ database và sử dụng RequestId để chống tạo trùng hóa đơn.
- Quản lý thay đổi database và tạo Stored Procedure bằng EF Core Migrations; cung cấp Swagger UI để kiểm thử API.
Frontend – MiniSupermarket.WinForms
Đóng vai trò Client giao tiếp với người dùng, chịu trách nhiệm:
- Duy trì giao diện Shell và điều hướng chức năng theo vai trò Admin, Cashier, Warehouse.
- Quản lý phiên đăng nhập và sử dụng ApiClientService dùng chung, gắn Bearer JWT vào yêu cầu HTTP.
- Tra cứu barcode qua API ADO.NET, thêm sản phẩm vào giỏ và tra cứu khách hàng theo số điện thoại.
- Hiển thị tổng tiền, tiền khách đưa, tiền thừa và gửi yêu cầu thanh toán đến Backend.
- Hiển thị kết quả, giữ giỏ khi thanh toán lỗi và làm mới giao dịch sau khi thanh toán thành công.
- Duy trì các màn hình quản lý và báo cáo nhanh đã có. Báo cáo Stored Procedure và API hủy hóa đơn được cung cấp phía Backend để kiểm thử trên Swagger.
🛠️ 2. Công nghệ sử dụng
Ngôn ngữ và nền tảng
- C# và .NET 8.0.
- Visual Studio 2022.
Phía Server – Backend
- ASP.NET Core Web API.
- ADO.NET với Microsoft.Data.SqlClient.
- SqlConnection, SqlCommand, SqlParameter, SqlDataReader, SqlTransaction.
- DataTable, SqlDataAdapter và CommandType.StoredProcedure.
- Entity Framework Core SQL Server, Design và Tools; Code-First Migrations.
- LINQ, async/await, Dependency Injection và Data Annotations.
- JWT Authentication, phân quyền theo vai trò và Swagger / OpenAPI.
Cơ sở dữ liệu
- Microsoft SQL Server, SQL Server Management Studio (SSMS).
- Stored Procedure, khóa ngoại và giao dịch dữ liệu.
- Các bảng chính: Categories, Products, Customers, Users, Orders, OrderDetails.
- Các thủ tục: sp_GetRevenueByCashier, sp_GetRevenueByDateRange, sp_GetTop5SellingProducts.
Phía Client – Frontend
- Windows Forms (.NET 8.0).
- HttpClient, System.Net.Http.Json và Bearer JWT.
- DataGridView, BindingSource, BindingList, ComboBox và các điều khiển nhập liệu.
📂 3. Cấu trúc Solution
Cấu trúc các file chính liên quan đến ứng dụng và Buổi 5; lược bỏ các file mẫu, .resx, file build và một số file Migration tự sinh:
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
│   │   ├── CashierRevenueDto.cs
│   │   ├── Category.cs
│   │   ├── Customer.cs
│   │   ├── Order.cs
│   │   ├── OrderDetail.cs
│   │   ├── Product.cs
│   │   └── Role.cs
│   ├── Migrations/
│   │   ├── 20261022112136_AddOrderRewardPointsBuoi5.cs
│   │   ├── 20261022112523_AddStoredProceduresBuoi5.cs
│   │   └── SupermarketDbContextModelSnapshot.cs
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
    ├── FormCategoryManagement.cs
    ├── FormCategoryManagement.Designer.cs
    ├── FormProductManagement.cs
    ├── FormProductManagement.Designer.cs
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
Trong cấu trúc hiện tại, SupermarketDbContext.cs và User.cs nằm trực tiếp trong project API. Lớp ApiClientService được khai báo trong SessionManager.cs của WinForms.
👩‍💻 4. Tác giả
Họ và tên: Nguyễn Thị Thu Hằng
Mã sinh viên: 2124110115
Lớp học phần: CCQ2411D
