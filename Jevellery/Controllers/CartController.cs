
using Jevellery.ViewModels.Cart;
using Jewellery.Business.Abstract;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Jevellery.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private UserManager<AppUser> _userManager;
        private readonly ICartProductService _cartProductService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(UserManager<AppUser> userManager, ICartProductService cartProductService, ICartService cartService, IProductService productService)
        {
            _userManager = userManager;
            _cartProductService = cartProductService;
            _cartService = cartService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var product = await _productService.GetAsync(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return  Redirect("/Account/Login");
            }
            Cart cart = await _cartService.GetAsync(c => c.UserId == user.Id);
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = user.Id
                };
                await _cartService.AddAsync(cart);
            }
            CartProduct cartProduct = await _cartProductService.Get(cp => cp.ProductId == productId && cp.CartId == cart.Id);
            if (cartProduct == null)
            {
                cartProduct = new CartProduct
                {
                    ProductId = productId,
                    CartId = cart.Id,
                    Quantity = quantity
                };
                await _cartProductService.AddAsync(cartProduct);
            }
            else
            {
                cartProduct.Quantity += quantity;
                await _cartProductService.UpdateAsync(cartProduct);
            }
            return Ok();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var cartProduct = await _cartProductService.Get(cp => cp.Id == id);
            if (cartProduct == null) return NotFound();
            await _cartProductService.DeleteAsync(cartProduct);
            return Ok();
        }

        public async Task<IActionResult> PlusCartItem(int id)
        {
            var cartProduct = await _cartProductService.Get(cp => cp.Id == id);
            if (cartProduct == null) return NotFound();
            cartProduct.Quantity++;
            await _cartProductService.UpdateAsync(cartProduct);
            return Ok();
        }

        public async Task<IActionResult> UpdateCartItem(int id, int quantity)
        {
            var cartProduct = await _cartProductService.Get(cp => cp.Id == id);
            if (cartProduct == null) return NotFound();
            cartProduct.Quantity = quantity;
            await _cartProductService.UpdateAsync(cartProduct);
            return Ok();
        }

        public async Task<IActionResult> MinusCartItem(int id)
        {
            var cartProduct = await _cartProductService.Get(cp => cp.Id == id);
            if (cartProduct == null) return NotFound();
            if (cartProduct.Quantity > 0)
                cartProduct.Quantity--;
            else
            {
                RedirectToAction("Remove", new { id = id });
            }
            await _cartProductService.UpdateAsync(cartProduct);
            return Ok();
        }

        public async Task<IActionResult> GetCartProducts()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return Unauthorized();
            var cart = await _cartService.GetAsync(c => c.UserId == user.Id);

            var cartProducts = await _cartProductService.GetCartProductsByCartId(cart.Id);
            var model = new CartVM
            {
                CartProducts = cartProducts,
                Sum = cartProducts.Sum(c =>
                {
                    decimal priceAfterDiscount = c.Product.Discount > 0 ?
                            c.Product.Price - (c.Product.Price * ((decimal)c.Product.Discount / 100)) :
                            c.Product.Price;
                    return Math.Round(priceAfterDiscount*c.Quantity, 2);
                }),
                Count = cartProducts.Count()
            };

            return PartialView("_CartContentPartialView", model);
        }
    }
}
