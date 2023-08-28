
using Dotnet.Web.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Web.Interfaces;

namespace Dotnet.Web.Controllers
{
    public class CartController : DotnetControllerBase
    {

        private readonly IProductService productService;
        private readonly ICartService cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }

        [Authorize("Bearer")]
        [HttpGet("/Cart")]
        [ProducesResponseType(typeof(GetUserCartResponseDto), 200)]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                return Ok(await cartService.GetUserCart());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete("/Cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CleanCart()
        {
            try
            {
                await cartService.CleanCart();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut("/Cart/{productId}")]
        public async Task<IActionResult> UpdateCart([FromRoute] int productId)
        {
            try
            {
                await productService.AddProductToCart(productId);
                return Ok();
            }
            catch(BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
