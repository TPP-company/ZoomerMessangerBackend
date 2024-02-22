using ZM.Infrastructure.RoutePrefix;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ZM.Api.Controllers;

[ApiController]
[RoutePrefix("api/")]
public abstract class ApiControllerBase : ControllerBase
{
#pragma warning disable CS8618 // ¬±¬à¬Ý¬Ö, ¬ß¬Ö ¬Õ¬à¬á¬å¬ã¬Ü¬Ñ¬ð¬ë¬Ö¬Ö ¬Ù¬ß¬Ñ¬é¬Ö¬ß¬Ú¬ñ NULL, ¬Õ¬à¬Ý¬Ø¬ß¬à ¬ã¬à¬Õ¬Ö¬â¬Ø¬Ñ¬ä¬î ¬Ù¬ß¬Ñ¬é¬Ö¬ß¬Ú¬Ö, ¬à¬ä¬Ý¬Ú¬é¬ß¬à¬Ö ¬à¬ä NULL, ¬á¬â¬Ú ¬Ó¬í¬ç¬à¬Õ¬Ö ¬Ú¬Ù ¬Ü¬à¬ß¬ã¬ä¬â¬å¬Ü¬ä¬à¬â¬Ñ. ¬£¬à¬Ù¬Þ¬à¬Ø¬ß¬à, ¬ã¬ä¬à¬Ú¬ä ¬à¬Ò¬ì¬ñ¬Ó¬Ú¬ä¬î ¬á¬à¬Ý¬Ö ¬Ü¬Ñ¬Ü ¬Õ¬à¬á¬å¬ã¬Ü¬Ñ¬ð¬ë¬Ö¬Ö ¬Ù¬ß¬Ñ¬é¬Ö¬ß¬Ú¬ñ NULL.
    private ISender _sender;
#pragma warning restore CS8618 // ¬±¬à¬Ý¬Ö, ¬ß¬Ö ¬Õ¬à¬á¬å¬ã¬Ü¬Ñ¬ð¬ë¬Ö¬Ö ¬Ù¬ß¬Ñ¬é¬Ö¬ß¬Ú¬ñ NULL, ¬Õ¬à¬Ý¬Ø¬ß¬à ¬ã¬à¬Õ¬Ö¬â¬Ø¬Ñ¬ä¬î ¬Ù¬ß¬Ñ¬é¬Ö¬ß¬Ú¬Ö, ¬à¬ä¬Ý¬Ú¬é¬ß¬à¬Ö ¬à¬ä NULL, ¬á¬â¬Ú ¬Ó¬í¬ç¬à¬Õ¬Ö ¬Ú¬Ù ¬Ü¬à¬ß¬ã¬ä¬â¬å¬Ü¬ä¬à¬â¬Ñ. ¬£¬à¬Ù¬Þ¬à¬Ø¬ß¬à, ¬ã¬ä¬à¬Ú¬ä ¬à¬Ò¬ì¬ñ¬Ó¬Ú¬ä¬î ¬á¬à¬Ý¬Ö ¬Ü¬Ñ¬Ü ¬Õ¬à¬á¬å¬ã¬Ü¬Ñ¬ð¬ë¬Ö¬Ö ¬Ù¬ß¬Ñ¬é¬Ö¬ß¬Ú¬ñ NULL.
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}