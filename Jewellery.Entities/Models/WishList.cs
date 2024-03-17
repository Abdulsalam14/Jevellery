﻿using Jewellery.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Entities.Models
{
    public class WishList:IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<WishlistProduct>? WishListProducts { get; set; }
    }
}
