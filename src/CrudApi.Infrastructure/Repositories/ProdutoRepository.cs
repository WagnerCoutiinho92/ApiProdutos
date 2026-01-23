using CrudApi.Application.Interfaces.Repositories;
using CrudApi.Domain.Entities;
using CrudApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Infrastructure.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }
}
