using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.ComponentModel.DataAnnotations;

using Microsoft.Data.SqlClient;


namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Cashier,Warehouse")]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }
        // GET /api/products/paged
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            // 1. Chuẩn hóa tham số đầu vào.
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1 || pageSize > 50)
                pageSize = 5;

            // 2. Khởi tạo truy vấn và lấy quan hệ danh mục.
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // 3. Tìm theo tên hoặc mã vạch.
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim();

                query = query.Where(p =>
                    p.ProductName.Contains(search) ||
                    p.Barcode.Contains(search));
            }

            // 4. Lọc theo danh mục.
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p =>
                    p.CategoryId == categoryId.Value);
            }

            // 5. Đếm sau khi áp dụng các điều kiện lọc.
            int totalItems = await query.CountAsync();

            int totalPages = (int)Math.Ceiling(
                (double)totalItems / pageSize);

            // Khi xóa hết sản phẩm của trang cuối,
            // đưa người dùng về trang cuối còn tồn tại.
            pageNumber = totalPages == 0
                ? 1
                : Math.Min(pageNumber, totalPages);

            // 6. Chỉ lấy các sản phẩm thuộc trang yêu cầu.
            var items = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null
                        ? p.Category.CategoryName
                        : string.Empty
                })
                .ToListAsync();

            // 7. Trả dữ liệu cùng thông tin phân trang.
            return Ok(new PagedResult<ProductItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
        }

        // GET /api/products?keyword=...&categoryId=...
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();

                query = query.Where(p =>
                    p.Barcode.Contains(keyword) ||
                    p.ProductName.Contains(keyword));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(
                    p => p.CategoryId == categoryId.Value);
            }

            var products = await query
                .OrderBy(p => p.ProductId)
                .Select(p => new
                {
                    p.ProductId,
                    p.Barcode,
                    p.ProductName,
                    p.Price,
                    p.StockQuantity,
                    p.CategoryId,
                    CategoryName = p.Category == null
                        ? ""
                        : p.Category.CategoryName
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET /api/products/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.ProductId == id)
                .Select(p => new
                {
                    p.ProductId,
                    p.Barcode,
                    p.ProductName,
                    p.Price,
                    p.StockQuantity,
                    p.CategoryId,
                    CategoryName = p.Category == null
                        ? ""
                        : p.Category.CategoryName
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });

            return Ok(product);
        }

        // GET /api/products/barcode/8931111111111
        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.Barcode == barcode.Trim())
                .Select(p => new
                {
                    p.ProductId,
                    p.Barcode,
                    p.ProductName,
                    p.Price,
                    p.StockQuantity,
                    p.CategoryId
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound(new
                {
                    message = "Không tìm thấy mã vạch này."
                });

            return Ok(product);
        }

        // POST /api/products
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create(
            [FromBody] ProductSaveRequest request)
        {
            string? error = await ValidateRequestAsync(request);

            if (error != null)
                return BadRequest(new { message = error });

            var product = new Product
            {
                Barcode = request.Barcode.Trim(),
                ProductName = request.ProductName.Trim(),
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CategoryId = request.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.ProductId },
                new { product.ProductId });
        }

        // PUT /api/products/1
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ProductSaveRequest request)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần sửa."
                });

            string? error = await ValidateRequestAsync(request, id);

            if (error != null)
                return BadRequest(new { message = error });

            product.Barcode = request.Barcode.Trim();
            product.ProductName = request.ProductName.Trim();
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/products/1
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần xóa."
                });
            }

            bool hasOrderDetails = await _context.OrderDetails
                .AnyAsync(d => d.ProductId == id);

            if (hasOrderDetails)
            {
                return Conflict(new
                {
                    message = "Sản phẩm đã có trong hóa đơn. " +
                              "Không thể xóa để bảo toàn lịch sử bán hàng."
                });
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sql && sql.Number == 547)
            {
                return Conflict(new
                {
                    message = "Sản phẩm đang được sử dụng, không thể xóa."
                });
            }
        }

        private async Task<string?> ValidateRequestAsync(
            ProductSaveRequest request,
            int? currentId = null)
        {
            if (string.IsNullOrWhiteSpace(request.Barcode))
                return "Mã vạch không được để trống.";

            if (string.IsNullOrWhiteSpace(request.ProductName))
                return "Tên sản phẩm không được để trống.";

            if (request.Price <= 0)
            {
                return "Giá bán phải lớn hơn 0.";
            }

            if (decimal.Round(request.Price, 2) != request.Price)
            {
                return "Giá bán chỉ được có tối đa 2 chữ số thập phân.";
            }

            if (request.StockQuantity < 0)
                return "Tồn kho không được âm.";

            bool categoryExists = await _context.Categories
                .AnyAsync(c => c.CategoryId == request.CategoryId);

            if (!categoryExists)
                return "Danh mục không tồn tại.";

            string barcode = request.Barcode.Trim();

            bool duplicate = await _context.Products.AnyAsync(p =>
                p.Barcode == barcode &&
                (!currentId.HasValue ||
                 p.ProductId != currentId.Value));

            if (duplicate)
                return "Mã vạch đã thuộc về một sản phẩm khác.";

            return null;
        }
    }

    public class ProductSaveRequest
    {
        [Required]
        [StringLength(50)]
        public string Barcode { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Range(
            typeof(decimal),
            "0.01",
            "9999999999999999.99",
            ErrorMessage = "Giá bán phải lớn hơn 0.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}