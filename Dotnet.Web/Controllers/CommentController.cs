using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Interfaces;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Dotnet.Web.Controllers
{
    public class CommentController : DotnetControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService) {
            _commentService = commentService;
        }


        [Authorize("Bearer")]
        [HttpPost("/Comment")]
        [Consumes("application/json", "text/plain", "text/json")]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> AddComment(AddCommentDto comment) {
            try
            {
                return Ok(await _commentService.AddComment(comment));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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

        [HttpGet("/Comment/{id}")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetComment([FromRoute] int id) {

            try
            {
                return Ok(await _commentService.GetComment(id));
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
