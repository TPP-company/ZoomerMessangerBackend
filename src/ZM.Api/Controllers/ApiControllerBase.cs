using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZM.Infrastructure.RoutePrefix;
namespace ZM.Api.Controllers;

[ApiController]
[RoutePrefix("api/")]
public abstract class ApiControllerBase : ControllerBase
{
	private ISender _sender = null!;
	protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}