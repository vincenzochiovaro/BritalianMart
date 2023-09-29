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

            //Note: This validation is not testing the data types, we are assuming that product.price is a number, that description is a string etc.

            //validation 1: product.description can't be less than 5 chars 
            if (product.Description.Length < 5 )
            {
                return false;
            }

            // Validation 2: productPrice must be > 0 
            if (product.Price <= 0)
            {
                return false;
            }

            //validation 3: Plu can't be empty
            if (product.Plu.Length <= 0 )
            {
                return false;
            }

            //validation 4: Category can't be empty
            if (product.Category.Length <= 0 )
            {
                return false;
            }

            //Validation 5: Brand Can't be empty

            if(product.Brand.Length <= 0)
            {
                return false;
            }

            return true;
        }

    }
}
