using Dotnet.Web.Interfaces;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dotnet.Web.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CommentService(
            AppDbContext context,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public int GetUserIdFromClaims()
        {
            var uridClaim = httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier);
            return (uridClaim != null ? int.Parse(uridClaim.Value) : -1);

        }


        public async Task<CommentDto> GetComment(int id) {
            var com = await context.Comments.Include(c => c.User).Include(c => c.Product).FirstOrDefaultAsync(com => com.Id == id);
            if (com == null) { throw new BadHttpRequestException("No such comment", 404); }
            var comDto = new CommentDto()
            {
                CommentId = id,
                ProductId = com.ProductId,
                Rating = com.Rating,
                Text = com.Text,
                ProductName = com.Product.Name,
                UserId = com.UserId,
                UserName = com.User.UserName
            };

            return comDto;
        }

        public async Task<IEnumerable<CommentDto>> GetComments(int productId)
        {
            var comments = await context.Comments.
                Include(c => c.User).
                Include(c => c.Product).
                Where(c=>c.ProductId == productId).ToListAsync();

            if (!comments.Any()) { throw new BadHttpRequestException("no such product", 404); }

            return comments.ConvertAll(com => new CommentDto()
            {
                CommentId = com.Id,
                ProductId = com.ProductId,
                Rating = com.Rating,
                Text = com.Text,
                ProductName = com.Product.Name,
                UserId = com.UserId,
                UserName = com.User.UserName

            });
        }

        public async Task<int> AddComment(AddCommentDto comment)
        {
            var userId = GetUserIdFromClaims();
            if (userId == -1) { throw new UnauthorizedAccessException("Unauthorized"); }

            var product = context.Products.FirstOrDefault(pr => pr.Id == comment.ProductId);
            if (product == null) { throw new BadHttpRequestException("Not found product", 422); }

            var user = context.Users.FirstOrDefault(user => user.Id == userId);
            var curMaxId = context.Comments.Select(c => c.Id).DefaultIfEmpty().Max();

            if (comment.Rating < 0 || comment.Rating > 5 )
            {
                throw new BadHttpRequestException("Invalid rating: rating must be > 0 and < 5", 422);
            }
            if (comment.Text!.Length > 200)
            {

                throw new BadHttpRequestException("Invalid text length: text length must be less than 200", 422);
            }

            var comToAdd = new Comment()
            {
                Id = curMaxId + 1,
                ProductId = comment.ProductId,
                Rating = comment.Rating,
                Text = comment.Text,
                UserId = user.Id,
                Product = product,
                User = user,

            };

            var res = await context.Comments.AddAsync(comToAdd);
            await context.SaveChangesAsync();
            return comToAdd.ProductId;
        }
    }
}
