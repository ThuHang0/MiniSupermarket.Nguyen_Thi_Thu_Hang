using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc với cơ sở dữ liệu SQL Server
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }

        // Khai báo các bảng dữ liệu ánh xạ từ Model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nạp sẵn 20 danh mục ban đầu vào SQL Server ngay khi tạo bảng
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo dẻo" },
                new Category { CategoryId = 2, CategoryName = "Nước giải khát", Description = "Nước ngọt, nước suối, trà đóng chai" },
                new Category { CategoryId = 3, CategoryName = "Sữa & Đồ uống dinh dưỡng", Description = "Sữa tươi, sữa hộp, sữa dinh dưỡng" },
                new Category { CategoryId = 4, CategoryName = "Mì & Thực phẩm ăn liền", Description = "Mì gói, cháo ăn liền, phở ăn liền" },
                new Category { CategoryId = 5, CategoryName = "Gia vị nấu ăn", Description = "Nước mắm, nước tương, muối, đường" },
                new Category { CategoryId = 6, CategoryName = "Dầu ăn", Description = "Dầu đậu nành, dầu hướng dương, dầu thực vật" },
                new Category { CategoryId = 7, CategoryName = "Gạo & Thực phẩm khô", Description = "Gạo, đậu, bột và các loại thực phẩm khô" },
                new Category { CategoryId = 8, CategoryName = "Đồ hộp", Description = "Cá hộp, thịt hộp, pate và thực phẩm đóng hộp" },
                new Category { CategoryId = 9, CategoryName = "Cà phê & Trà", Description = "Cà phê hòa tan, cà phê rang xay và trà" },
                new Category { CategoryId = 10, CategoryName = "Bánh mì & Bánh ngọt", Description = "Bánh mì, bánh bông lan và bánh ngọt" },
                new Category { CategoryId = 11, CategoryName = "Kem & Thực phẩm đông lạnh", Description = "Kem, xúc xích và các loại thực phẩm đông lạnh" },
                new Category { CategoryId = 12, CategoryName = "Đồ dùng nhà bếp", Description = "Màng bọc thực phẩm, túi đựng và đồ dùng nhà bếp" },
                new Category { CategoryId = 13, CategoryName = "Giấy & Khăn giấy", Description = "Khăn giấy, giấy vệ sinh và giấy lau" },
                new Category { CategoryId = 14, CategoryName = "Chăm sóc cá nhân", Description = "Dầu gội, sữa tắm và sản phẩm vệ sinh cá nhân" },
                new Category { CategoryId = 15, CategoryName = "Kem đánh răng & Bàn chải", Description = "Kem đánh răng, bàn chải và sản phẩm chăm sóc răng miệng" },
                new Category { CategoryId = 16, CategoryName = "Nước giặt & Nước xả", Description = "Nước giặt, bột giặt và nước xả vải" },
                new Category { CategoryId = 17, CategoryName = "Nước rửa chén & Vệ sinh", Description = "Nước rửa chén và các sản phẩm vệ sinh nhà cửa" },
                new Category { CategoryId = 18, CategoryName = "Đồ dùng học tập", Description = "Bút, vở, thước và các dụng cụ học tập" },
                new Category { CategoryId = 19, CategoryName = "Đồ dùng cá nhân", Description = "Khẩu trang, bàn chải, lược và đồ dùng cá nhân" },
                new Category { CategoryId = 20, CategoryName = "Đồ gia dụng", Description = "Các vật dụng gia đình và đồ dùng sinh hoạt hằng ngày" }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    CategoryId = 1,
                    Barcode = "8931111111111",
                    ProductName = "Snack Khoai Tây Lay's Vị Tự Nhiên",
                    Price = 12000,
                    StockQuantity = 50
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 2,
                    Barcode = "8932222222222",
                    ProductName = "Nước Ngọt Coca-Cola Lon 320ml",
                    Price = 10000,
                    StockQuantity = 120
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 3,
                    Barcode = "8933333333333",
                    ProductName = "Sữa Tươi TH True Milk 180ml",
                    Price = 8500,
                    StockQuantity = 200
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 4,
                    Barcode = "8934444444444",
                    ProductName = "Mì Hảo Hảo Tôm Chua Cay",
                    Price = 4500,
                    StockQuantity = 500
                },
                new Product
                {
                    ProductId = 5,
                    CategoryId = 5,
                    Barcode = "8935555555555",
                    ProductName = "Nước Mắm Nam Ngư Đệ Nhị 900ml",
                    Price = 25000,
                    StockQuantity = 30
                },
                new Product
                {
                    ProductId = 6,
                    CategoryId = 6,
                    Barcode = "8936666666666",
                    ProductName = "Dầu Ăn Simply Đậu Nành 1 Lít",
                    Price = 55000,
                    StockQuantity = 40
                },
                new Product
                {
                    ProductId = 7,
                    CategoryId = 7,
                    Barcode = "8930000000007",
                    ProductName = "Gạo ST25 Túi 5kg",
                    Price = 180000,
                    StockQuantity = 35
                },
                new Product
                {
                    ProductId = 8,
                    CategoryId = 8,
                    Barcode = "8930000000008",
                    ProductName = "Cá Hộp Ba Cô Gái 155g",
                    Price = 22000,
                    StockQuantity = 65
                },
                new Product
                {
                    ProductId = 9,
                    CategoryId = 9,
                    Barcode = "8930000000009",
                    ProductName = "Cà Phê G7 Hòa Tan Hộp 20 Gói",
                    Price = 48000,
                    StockQuantity = 75
                },
                new Product
                {
                    ProductId = 10,
                    CategoryId = 10,
                    Barcode = "8930000000010",
                    ProductName = "Bánh Bông Lan Solite Hộp 360g",
                    Price = 42000,
                    StockQuantity = 45
                },
                new Product
                {
                    ProductId = 11,
                    CategoryId = 11,
                    Barcode = "8930000000011",
                    ProductName = "Kem Merino Vani Hộp 450ml",
                    Price = 45000,
                    StockQuantity = 30
                },
                new Product
                {
                    ProductId = 12,
                    CategoryId = 12,
                    Barcode = "8930000000012",
                    ProductName = "Màng Bọc Thực Phẩm Ringo 30cm",
                    Price = 28000,
                    StockQuantity = 55
                },
                new Product
                {
                    ProductId = 13,
                    CategoryId = 13,
                    Barcode = "8937777777777",
                    ProductName = "Lốc 6 Cuộn Giấy Vệ Sinh Bless You",
                    Price = 45000,
                    StockQuantity = 60
                },
                new Product
                {
                    ProductId = 14,
                    CategoryId = 14,
                    Barcode = "8938888888888",
                    ProductName = "Dầu Gội Clear Men Bạc Hà 630g",
                    Price = 165000,
                    StockQuantity = 25
                },
                new Product
                {
                    ProductId = 15,
                    CategoryId = 15,
                    Barcode = "8939999999999",
                    ProductName = "Kem Đánh Răng P/S Trà Xanh 230g",
                    Price = 38000,
                    StockQuantity = 45
                },
                new Product
                {
                    ProductId = 16,
                    CategoryId = 16,
                    Barcode = "8930000000000",
                    ProductName = "Nước Giặt Omo Matic Cửa Trước 3.6kg",
                    Price = 210000,
                    StockQuantity = 15
                },
                new Product
                {
                    ProductId = 17,
                    CategoryId = 17,
                    Barcode = "8930000000017",
                    ProductName = "Nước Rửa Chén Sunlight Chanh 750ml",
                    Price = 32000,
                    StockQuantity = 80
                },
                new Product
                {
                    ProductId = 18,
                    CategoryId = 18,
                    Barcode = "8930000000018",
                    ProductName = "Vở Học Sinh Campus 200 Trang",
                    Price = 18000,
                    StockQuantity = 150
                },
                new Product
                {
                    ProductId = 19,
                    CategoryId = 19,
                    Barcode = "8930000000019",
                    ProductName = "Khẩu Trang Y Tế Hộp 50 Cái",
                    Price = 35000,
                    StockQuantity = 100
                },
                new Product
                {
                    ProductId = 20,
                    CategoryId = 20,
                    Barcode = "8930000000020",
                    ProductName = "Hộp Nhựa Đựng Thực Phẩm Duy Tân 1 Lít",
                    Price = 25000,
                    StockQuantity = 70
                }
            );



            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn Anh",
                    PhoneNumber = "0901122334",
                    Address = "125 Nguyễn Trãi, Phường Bến Thành, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 150
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bích Ngọc",
                    PhoneNumber = "0918877665",
                    Address = "45 Lê Lợi, Phường Sài Gòn, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 50
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn Cường",
                    PhoneNumber = "0983344556",
                    Address = "78 Nguyễn Văn Linh, Phường Tân Hưng, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 10
                },
                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Diễm My",
                    PhoneNumber = "0934455667",
                    Address = "230 Điện Biên Phủ, Phường Gia Định, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 200
                },
                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Văn Đức",
                    PhoneNumber = "0977788990",
                    Address = "56 Phan Văn Trị, Phường Gò Vấp, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 80
                },
                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Thanh Hương",
                    PhoneNumber = "0961234567",
                    Address = "102 Quang Trung, Phường Thông Tây Hội, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 25
                },
                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Văn Giang",
                    PhoneNumber = "0923456789",
                    Address = "88 Nguyễn Xí, Phường Bình Lợi Trung, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 175
                },
                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Thị Kim Ngân",
                    PhoneNumber = "0902233445",
                    Address = "15 Cách Mạng Tháng Tám, Phường Bàn Cờ, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 65
                },
                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Đỗ Minh Khang",
                    PhoneNumber = "0913344556",
                    Address = "67 Trường Chinh, Phường Tân Bình, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 30
                },
                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Nguyễn Thị Thu Hà",
                    PhoneNumber = "0984455667",
                    Address = "190 Hoàng Văn Thụ, Phường Phú Nhuận, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 220
                },
                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Trần Quốc Bảo",
                    PhoneNumber = "0935566778",
                    Address = "25 Lạc Long Quân, Phường Hòa Bình, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 90
                },
                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Lê Thị Minh Châu",
                    PhoneNumber = "0976677889",
                    Address = "140 Âu Cơ, Phường Tân Hòa, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 15
                },
                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Phan Thanh Tùng",
                    PhoneNumber = "0967788991",
                    Address = "39 Võ Văn Ngân, Phường Thủ Đức, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 300
                },
                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Huỳnh Thị Lan Anh",
                    PhoneNumber = "0928899001",
                    Address = "81 Kha Vạn Cân, Phường Linh Xuân, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 110
                },
                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Ngô Văn Phúc",
                    PhoneNumber = "0909900112",
                    Address = "210 Nguyễn Duy Trinh, Phường Long Trường, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 35
                },
                new Customer
                {
                    CustomerId = 16,
                    CustomerName = "Mai Thị Ngọc Hân",
                    PhoneNumber = "0911011223",
                    Address = "72 Tô Ngọc Vân, Phường Tam Bình, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 250
                },
                new Customer
                {
                    CustomerId = 17,
                    CustomerName = "Trương Minh Quân",
                    PhoneNumber = "0982122334",
                    Address = "48 Phạm Văn Đồng, Phường Hiệp Bình, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 75
                },
                new Customer
                {
                    CustomerId = 18,
                    CustomerName = "Đinh Thị Mỹ Linh",
                    PhoneNumber = "0933233445",
                    Address = "115 Lê Văn Việt, Phường Tăng Nhơn Phú, TP. Hồ Chí Minh",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 40
                },
                new Customer
                {
                    CustomerId = 19,
                    CustomerName = "Cao Văn Thành",
                    PhoneNumber = "0974344556",
                    Address = "93 Nguyễn Thị Thập, Phường Tân Mỹ, TP. Hồ Chí Minh",
                    MembershipRank = "Vàng",
                    RewardPoints = 180
                },
                new Customer
                {
                    CustomerId = 20,
                    CustomerName = "Dương Thị Thùy Trang",
                    PhoneNumber = "0965455667",
                    Address = "160 Huỳnh Tấn Phát, Phường Tân Thuận, TP. Hồ Chí Minh",
                    MembershipRank = "Bạc",
                    RewardPoints = 95
                }
            );

        }
    }
}
