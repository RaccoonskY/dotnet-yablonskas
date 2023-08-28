using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Interfaces;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dotnet.Web.Controllers
{
    public class OrderController : DotnetControllerBase
    {

        private readonly IOrderService orderService;
        UserManager<User> userManager;
        public OrderController(IOrderService orderService, UserManager<User> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
        }
        private int GetUserIdFromClaims()
        {
            var uridClaim = User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier);
            return (uridClaim != null ? int.Parse(uridClaim.Value) : -1);

        }

        [Authorize("Bearer")]
        [HttpGet("/Order")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [Produces("text/plain", "application/json", "text/json")]
        public async Task<IActionResult> GetOrder() {
            try
            {
                return Ok(await orderService.GetOrder());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost("/Order")]
        [Produces("text/plain", "application/json", "text/json")]
        public async Task<IActionResult> PostOrder() {
            try
            {
                var orderId = await orderService.CreateOrder();
                return Ok(orderId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("Bearer", Roles ="Admin")]
        [HttpPut("/Order/Pay/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutOrderPayment([FromRoute] int id)
        {
            try
            {
                await orderService.MoveOrderStatus(id, OrderStatus.Payed);
                return Ok("Success");
            }
            catch (BadHttpRequestException)
            {
                return StatusCode(422);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
           
        }

        [Authorize("Bearer", Roles = "Admin")]
        [HttpPut("/Order/Ship/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutOrderShipment([FromRoute] int id) {

            try
            {
                await orderService.MoveOrderStatus(id, OrderStatus.Shipped);
                return Ok("Success");
            }
            catch (BadHttpRequestException)
            {
                return StatusCode(422);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("Bearer", Roles = "Admin")]
        [HttpPut("/Order/Dispute/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutOrderDispute([FromRoute] int id) {
            try
            {
                await orderService.MoveOrderStatus(id, OrderStatus.Disputed);
                return Ok("Success");
            }
            catch (BadHttpRequestException)
            {
                return StatusCode(422);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("Bearer", Roles = "Admin")]
        [HttpPut("/Order/Complete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutOrderComplete([FromRoute] int id) {
            try
            {
                await orderService.MoveOrderStatus(id, OrderStatus.Completed);
                return Ok("Success");
            }
            catch (BadHttpRequestException)
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