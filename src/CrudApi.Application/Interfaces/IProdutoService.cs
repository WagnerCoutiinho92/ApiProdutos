using CrudApi.Application.DTOs;

namespace CrudApi.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoResponseDto>> Listar();
    Task Criar(ProdutoCreateDto dto);
    Task Atualizar(ProdutoUpdateDto dto);
    Task Excluir(int id);
}