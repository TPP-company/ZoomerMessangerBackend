using ZM.Domain.Common;

namespace ZM.Domain.Entities;
public class User : Entity
{
    public string UserName { get; set; } = null!;
    public string? About { get; set; }
    public Guid ExternalId { get; set; }
    public Guid? AvatarId { get; set; }
    public DateTime DateOfRegistration { get; set; }
}
