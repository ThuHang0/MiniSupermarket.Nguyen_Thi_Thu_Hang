namespace MiniSupermarket.API.Models
{
    // Thông tin sản phẩm hiển thị trên giao diện.
    public class ProductItemDto
    {
        public int ProductId { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }

    // Kết quả phân trang dùng chung.
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public List<T> Items { get; set; } = new();
    }
}