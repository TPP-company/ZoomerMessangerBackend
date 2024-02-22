using ZM.Infrastructure.RoutePrefix;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ZM.Api.Controllers;

[ApiController]
[RoutePrefix("api/")]
public abstract class ApiControllerBase : ControllerBase
{
#pragma warning disable CS8618 // ����ݬ�, �߬� �լ����ܬѬ��֬� �٬߬Ѭ�֬߬ڬ� NULL, �լ�ݬج߬� ���լ֬�جѬ�� �٬߬Ѭ�֬߬ڬ�, ���ݬڬ�߬�� ��� NULL, ���� �Ӭ���լ� �ڬ� �ܬ�߬����ܬ����. ����٬ެ�ج߬�, ����ڬ� ��Ҭ��Ӭڬ�� ���ݬ� �ܬѬ� �լ����ܬѬ��֬� �٬߬Ѭ�֬߬ڬ� NULL.
    private ISender _sender;
#pragma warning restore CS8618 // ����ݬ�, �߬� �լ����ܬѬ��֬� �٬߬Ѭ�֬߬ڬ� NULL, �լ�ݬج߬� ���լ֬�جѬ�� �٬߬Ѭ�֬߬ڬ�, ���ݬڬ�߬�� ��� NULL, ���� �Ӭ���լ� �ڬ� �ܬ�߬����ܬ����. ����٬ެ�ج߬�, ����ڬ� ��Ҭ��Ӭڬ�� ���ݬ� �ܬѬ� �լ����ܬѬ��֬� �٬߬Ѭ�֬߬ڬ� NULL.
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}