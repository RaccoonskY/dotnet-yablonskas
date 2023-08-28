using Dotnet.Web.Interfaces;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IIS;
using Dotnet.Web.Exceptions;

namespace Dotnet.Web.Controllers
{
    public class ProductsController : DotnetControllerBase
    {
        private readonly IProductService productService;

        private readonly ICommentService commentService;
        public ProductsController(IProductService productService, ICommentService commentService)
        {
            this.productService = productService;
            this.commentService = commentService;
        }

        [HttpGet("/Products")]
        [ProducesResponseType(typeof(Product[]), 200)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetProducts() {
            return Ok(await productService.GetListAsync(new PagingDto()));
        }

        [HttpGet("/Products/{productId}/comments")]
        [ProducesResponseType(typeof(CommentDto[]), 200)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetProductComments(int productId) {

            try
            {
                return Ok(await commentService.GetComments(productId));
            }
            catch(Microsoft.AspNetCore.Http.BadHttpRequestException ex)
            {
                return StatusCode(422);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Products/{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetProductById(int id) {
            try
            {
                return Ok(await productService.GetProduct(id));
            }
            catch(Microsoft.AspNetCore.Http.BadHttpRequestException ex)
            {
                return StatusCode(422);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
