using ZM.Infrastructure.RoutePrefix;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ZM.Api.Controllers;

[ApiController]
[RoutePrefix("api/")]
public abstract class ApiControllerBase : ControllerBase
{
#pragma warning disable CS8618 // Поле, не допускающе?значен? NULL, должно содержат?значение, отличное от NULL, пр?выходе из конструктора. Возможно, стои?об?вить поле ка?допускающе?значен? NULL.
    private ISender _sender;
#pragma warning restore CS8618 // Поле, не допускающе?значен? NULL, должно содержат?значение, отличное от NULL, пр?выходе из конструктора. Возможно, стои?об?вить поле ка?допускающе?значен? NULL.

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}