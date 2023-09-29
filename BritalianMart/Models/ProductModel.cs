using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritalianMart.Models
{
    public class Entity
    {
        public string Id { get; set; }
        public DateTime Created { get; set;}
        public DateTime Modified { get; set; }
    }


    public class ProductModel : Entity
    {
        public string Plu { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
    }



}
