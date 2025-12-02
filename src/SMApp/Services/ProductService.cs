using SMApp.Data;
using SMApp.Extensions;
using SMApp.Models;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class ProductService(SalesManagementDbContext _dbContext) : IProductService
{
    public async Task<List<ProductModel>> GetProducts()
    {
        try
        {
            var products = await _dbContext.Products.Convert(_dbContext);
            return products;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
