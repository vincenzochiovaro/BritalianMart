using BritalianMart.Interfaces;
using BritalianMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritalianMart.Services
{
    public class ProductValidator : IProductValidator
    {
        public  bool IsValid(ProductModel product)
        {

            //validation 1: product.description can't be less than 5 chars 
            if (product.Description.Length < 5 )
            {
                return false;
            }

            //validation 2: productPrice must be > 0 or free
            decimal priceValueParsed;
            if (product.Price.ToLower() != "free" && (!decimal.TryParse(product.Price, out priceValueParsed) || priceValueParsed <= 0))
            {

                return false;
            }

            //validation 3: Plu can't be empty
            if (product.Plu.Length <= 0 )
            {
                return false;
            }

            return true;
        }

    }
}
