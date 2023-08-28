using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Web.Dto;

public class PagingDto
{
    [FromQuery]
    public int Take { get; set; } = 50;

    [FromQuery]
    public int Page { get; set; } = 1;
}