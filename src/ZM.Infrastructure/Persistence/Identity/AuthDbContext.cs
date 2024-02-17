using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZM.Infrastructure.Authentication.Entities;

namespace ZM.Infrastructure.Persistence.Identity;

internal class AuthDbContext : IdentityDbContext<AuthUser, AuthRole, Guid>
{
    public const string DefaultSchema = "identity";

    public AuthDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(DefaultSchema);
        base.OnModelCreating(builder);
    }
}