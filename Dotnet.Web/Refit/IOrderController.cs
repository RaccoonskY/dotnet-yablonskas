using Dotnet.Web.Dto;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Dotnet.Web.Refit;

public interface IOrderController
{
    [Get("/Order")]
    public Task<OrderDto> Get();

    [Post("/Order")]
    public Task<int> Create();

    [Put("/Order/Pay/{id}")]
    public Task UpdatePay([FromRoute] int id);

    [Put("/Order/Ship/{id}")]
    public Task UpdateShip([FromRoute] int id);

    [Put("/Order/Dispute/{id}")]
    public Task UpdateDispute([FromRoute] int id);

    [Put("/Order/Complete/{id}")]
    public Task UpdateComplete([FromRoute] int id);
}