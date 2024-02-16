using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Persistence;
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
}
