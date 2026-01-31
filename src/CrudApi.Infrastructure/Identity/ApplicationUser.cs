using CrudApi.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CrudApi.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<string>, IAuditableEntity
{
    public string Nome { get; set; } = null!;

    public bool Ativo { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

