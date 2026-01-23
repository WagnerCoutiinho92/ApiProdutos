
using CrudApi.Domain.Common;

namespace CrudApi.Domain.Entities;

public class Produto : IAuditableEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Descricao { get; set; } = "";
    public decimal Preco { get; set; }
    public bool Ativo { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

