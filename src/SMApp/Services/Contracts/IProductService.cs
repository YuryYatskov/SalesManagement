using SMApp.Models;

namespace SMApp.Services.Contracts;

public interface IProductService
{
    Task<List<ProductModel>> GetProducts();
}
