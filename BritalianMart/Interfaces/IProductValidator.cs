using BritalianMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritalianMart.Interfaces
{
    public interface IProductValidator
    {
       bool IsValid(ProductModel productToValidate);
    }
}
