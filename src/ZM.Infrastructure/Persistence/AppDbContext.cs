using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZM.Infrastructure.Authentication.Entities;

namespace ZM.Infrastructure.Persistence;
internal class AppDbContext : IdentityDbContext<AuthUser, AuthRole, Guid>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}
