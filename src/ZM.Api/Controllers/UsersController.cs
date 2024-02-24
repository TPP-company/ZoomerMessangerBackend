using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Application.UseCases.Users.GetOwnUser;
using ZM.Application.UseCases.Users.UpdateUser;
using ZM.Common.Results;
using ZM.Domain.Entities;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи пользователей.
/// </summary>
[ApiController]
[Route("users")]
public class UsersController(IDbContext _dbContext) : ApiControllerBase
{

	/// <summary>
	/// Получить всех пользователей.
	/// </summary>
	[HttpGet]
	public Task<User[]> GetAll()
		=> _dbContext.Set<User>().ToArrayAsync();

	/// <summary>
	/// Обновить информацию о себе
	/// </summary>
	/// <param name="userCommand">string About, Guid AvatarId</param>
	[HttpPatch("own")]
	public Task<Result<ResultDataEmpty>> UpdateOwnProfile([FromBody] UpdateUserCommand userCommand)
	{
		return Sender.Send(userCommand);
	}

	
	/// <summary>
	/// Получение информации о текущем пользователе
	/// </summary>
	/// <returns></returns>
	[HttpGet("own")]
	public Task<Result<GetOwnUserResponse>> GetCurrentUser()
	{
		return Sender.Send(new GetOwnUserQuery());
	}
}
