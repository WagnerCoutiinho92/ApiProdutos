using CrudApi.Data;
using CrudApi.Domain.Entities;
using CrudApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Produto>> Listar() =>
            await _context.Produtos.AsNoTracking().ToListAsync();

        public Task<Produto?> ObterPorId(int id) =>
            _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

        public async Task Adicionar(Produto produto)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Produto produto)
        {
            _context.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
            var produto = await ObterPorId(id);
            if (produto == null) return;
            produto.Excluido = true;
            await _context.SaveChangesAsync();
        }
    }
}