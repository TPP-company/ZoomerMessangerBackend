﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Application.UseCases.Users.UpdateUser;
using ZM.Common.Results;
using ZM.Domain.Entities;
using ZM.Infrastructure.Persistence.App.Migrations;

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
    /// <returns></returns>
    [HttpPatch("own/update")]
    public Task<Result<ResultDataEmpty>> UpdateOwnProfile([FromBody] UpdateUserCommand userCommand) {
        return Sender.Send(userCommand);     
    }
}
