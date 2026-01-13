
using CrudApi.Application.Interfaces;
using CrudApi.Domain.Entities;
using CrudApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> Listar() =>
        await _context.Produtos.Where(p => p.Ativo).ToListAsync();

    public async Task<Produto?> ObterPorId(int id) =>
        await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);

    public async Task Adicionar(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDelete(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            produto.Ativo = false;
            await _context.SaveChangesAsync();
        }
    }
}
