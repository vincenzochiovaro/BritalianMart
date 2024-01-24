using System.Threading.Tasks;
using BritalianMart.Models;
using System.Collections.Generic;

namespace BritalianMart.Interfaces
{
    // ORM - EntityFramework, nHibernate
    public interface IProductCatalog
    {
        Task Add(ProductModel productModel);
        Task Update(ProductModel productModel);
        Task<ProductModel> GetById(string id);
        Task<IEnumerable<ProductModel>> GetAll();
    }
}

