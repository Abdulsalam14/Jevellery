using Jewellery.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Entities.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<CartProduct>? CartProducts { get; set; }
        public bool IsNewArrival { get; set; }
        public bool IsFeatured { get; set; }
    }
}
