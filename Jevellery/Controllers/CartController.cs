using Jevellery.Models;
using Jevellery.Services.Abstract;
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
            var product = await _productService.Get(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return Unauthorized();
            Cart cart = await _cartService.Get(c => c.UserId == user.Id);
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
                cartProduct.Quantity+=quantity;
                await _cartProductService.UpdateAsync(cartProduct);
            }
            return Ok();
        }
        public async Task<IActionResult> GetCartProducts()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return Unauthorized();
            var cart = await _cartService.Get(c => c.UserId == user.Id);

            var cartProducts= await _cartProductService.GetCartProductsByCartId(cart.Id);
            return PartialView("_CartContentPartialView",cartProducts);
        }
    }
}
