using CrudApi.Domain.Entities;

namespace CrudApi.Infrastructure.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> Listar();
    Task<Produto?> ObterPorId(int id);
    Task Adicionar(Produto produto);
    Task Atualizar(Produto produto);
    Task SoftDelete(int id);
}