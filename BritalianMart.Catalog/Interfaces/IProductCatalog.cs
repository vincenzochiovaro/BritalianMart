using BritalianMart.Models;

namespace BritalianMart.Catalog.Interfaces
{
    public interface IProductCatalog
    {
        Task Add(ProductModel productModel);
        Task Update(ProductModel productModel);
        Task<ProductModel> GetById(string id);
        Task<IEnumerable<ProductModel>> GetAll();
    }
}

