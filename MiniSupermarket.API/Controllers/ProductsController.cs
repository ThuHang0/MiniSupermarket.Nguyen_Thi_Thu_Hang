using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

    namespace MiniSupermarket.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ProductsController : ControllerBase
        {
            private readonly SupermarketDbContext _context;

            public ProductsController(SupermarketDbContext context)
            {
                _context = context;
            }

            // GET /api/products
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var products = await _context.Products
                    .AsNoTracking()
                    .OrderBy(p => p.ProductId)
                    .Select(p => new
                    {
                        p.ProductId,
                        p.Barcode,
                        p.ProductName,
                        p.Price,
                        p.StockQuantity,
                        p.CategoryId
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
                        p.CategoryId
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy sản phẩm!"
                    });
                }

                return Ok(product);
            }

            // GET /api/products/barcode/8931111111111
            [HttpGet("barcode/{barcode}")]
            public async Task<IActionResult> GetByBarcode(string barcode)
            {
                barcode = barcode.Trim();

                var product = await _context.Products
                    .AsNoTracking()
                    .Where(p => p.Barcode == barcode)
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
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy sản phẩm có mã vạch này!"
                    });
                }

                return Ok(product);
            }

            // POST /api/products
            [HttpPost]
            public async Task<IActionResult> Create(
                [FromBody] Product input)
            {
                string? error = await ValidateProductAsync(input);

                if (error != null)
                {
                    return BadRequest(new { message = error });
                }

                var product = new Product
                {
                    Barcode = input.Barcode.Trim(),
                    ProductName = input.ProductName.Trim(),
                    Price = input.Price,
                    StockQuantity = input.StockQuantity,
                    CategoryId = input.CategoryId
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = product.ProductId },
                    new
                    {
                        product.ProductId,
                        product.Barcode,
                        product.ProductName,
                        product.Price,
                        product.StockQuantity,
                        product.CategoryId
                    });
            }

            // PUT /api/products/1
            [HttpPut("{id:int}")]
            public async Task<IActionResult> Update(
                int id,
                [FromBody] Product input)
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy sản phẩm cần sửa!"
                    });
                }

                string? error = await ValidateProductAsync(input, id);

                if (error != null)
                {
                    return BadRequest(new { message = error });
                }

                product.Barcode = input.Barcode.Trim();
                product.ProductName = input.ProductName.Trim();
                product.Price = input.Price;
                product.StockQuantity = input.StockQuantity;
                product.CategoryId = input.CategoryId;

                await _context.SaveChangesAsync();

                return NoContent();
            }

            // DELETE /api/products/1
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(int id)
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy sản phẩm cần xóa!"
                    });
                }

                _context.Products.Remove(product);

                try
                {
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateException)
                {
                    return Conflict(new
                    {
                        message =
                            "Không thể xóa sản phẩm. Hãy kiểm tra dữ liệu liên quan."
                    });
                }
            }

            // Kiểm tra dữ liệu trước khi thêm hoặc sửa
            private async Task<string?> ValidateProductAsync(
                Product input,
                int? currentId = null)
            {
                if (string.IsNullOrWhiteSpace(input.Barcode))
                    return "Mã vạch không được để trống!";

                if (string.IsNullOrWhiteSpace(input.ProductName))
                    return "Tên sản phẩm không được để trống!";

                if (input.Price <= 0)
                    return "Giá sản phẩm phải lớn hơn 0!";

                if (input.StockQuantity < 0)
                    return "Số lượng tồn kho không được âm!";

                bool categoryExists = await _context.Categories
                    .AnyAsync(c => c.CategoryId == input.CategoryId);

                if (!categoryExists)
                    return "Danh mục không tồn tại!";

                string barcode = input.Barcode.Trim();

                bool barcodeExists = await _context.Products
                    .AnyAsync(p =>
                        p.Barcode == barcode &&
                        (!currentId.HasValue ||
                         p.ProductId != currentId.Value));

                if (barcodeExists)
                    return "Mã vạch đã được sử dụng!";

                return null;
            }
        }
    }