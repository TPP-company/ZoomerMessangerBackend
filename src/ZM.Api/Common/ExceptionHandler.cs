using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.Common.Exceptions;

public class ExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		switch (exception)
		{
			case ValidationException:
				httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				break;
			case ResourceNotFoundException:
				httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
				break;
		}

		var details = new ProblemDetails
		{
			Status = httpContext.Response.StatusCode,
			Type = exception.GetType().Name,
			Title = "An unexpected error occurred",
			Detail = exception.Message,
			Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
		};

		await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

		return true;
	}
}
