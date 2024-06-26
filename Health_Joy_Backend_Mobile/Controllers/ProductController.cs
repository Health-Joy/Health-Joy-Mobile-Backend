using Health_Joy_Mobile_Backend.Data;
using Health_Joy_Mobile_Backend.Schema;
using Microsoft.AspNetCore.Mvc;
using Health_Joy_Backend_Mobile.Applications.ProductOperations.CreateProduct;
using Health_Joy_Backend_Mobile.Applications.ProductOperations.GetProduct;
using Health_Joy_Backend_Mobile.Applications.ProductOperations.UpdateProduct;
using Health_Joy_Backend_Mobile.Common;

namespace Health_Joy_Backend_Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct(ProductRequest productRequest)
        {
            var createProductCommand = new CreateProductCommand(_context, productRequest);
            return await createProductCommand.Handle();
        }
        
        [HttpGet]
        public async Task<ApiResponse<ProductResponse>> GetProduct(string? productBarcode)
        {
            var getProductQuery = new GetProductbyBarcodeQuery(_context);
            if (productBarcode is null)
            {
                return new ApiResponse<ProductResponse>("barcode is null");
            }
            return await getProductQuery.ExecuteAsync(productBarcode);
        }
        [HttpGet("NotApproved")]
        public async Task<ApiResponse<List<ProductResponse>>> GetProductNotApproved()
        {
            var getProductNotApprovedQuery = new GetAllNotApprovedProductsQuery(_context);
            return await getProductNotApprovedQuery.ExecuteAsync();

        }
        
        [HttpGet("GetAllProducts")]
        public async Task<ApiResponse<List<ProductResponse>>> GetAllProducts()
        {
            var getProductsQuery = new GetAllProducts(_context);
            return await getProductsQuery.ExecuteAsync();

        }
        [HttpPut("Approve/{id}")]
        public async Task<ApiResponse> GetProduct(int id ,ProductRequest request )
        {
            var approveProduct = new UpdateProductCommand(_context,request,id);
            return await approveProduct.Handle();
        }
    }
}