using BritalianMart.Models;

namespace BritalianMart.Reports.Interfaces
{
    public interface IProductsReport
    {
        Task<List<ProductModel>> GetReport();
    }

}
