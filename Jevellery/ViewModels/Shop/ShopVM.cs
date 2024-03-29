﻿using Jevellery.WebUI.ViewModels.Shop;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jevellery.ViewModels.Shop
{
    public class ShopVM
    {
        public List<ProductShopVM> Products { get; set; }
        public List<SelectListItem> Selects { get; set; }
        public List<Category> Categories { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public string Sort { get; set; }
        public int FilterMax { get; set; }
        public int FilterMin { get; set; }
        public int CategoryId { get; set; }
        public int MaxProductPrice {  get; set; }
        public int MinProductPrice { get; set; }
    }
}
