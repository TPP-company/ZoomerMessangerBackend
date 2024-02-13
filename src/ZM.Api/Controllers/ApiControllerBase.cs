using Template.Infrastructure.RoutePrefix;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ZM.Api.Controllers;

[ApiController]
[RoutePrefix("api/")]
public abstract class ApiControllerBase : ControllerBase
{
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    private ISender _sender;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}