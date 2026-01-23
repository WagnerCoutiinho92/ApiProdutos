using CrudApi.Application.DTOs;
using CrudApi.Application.Interfaces.Repositories;
using CrudApi.Application.Interfaces.Services;
using CrudApi.Domain.Entities;

namespace CrudApi.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repo;

    public ProdutoService(IProdutoRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public async Task<ProdutoResponseDto> CreateAsync(ProdutoCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (dto.Preco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        var produto = new Produto
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Preco = dto.Preco,
            Ativo = true,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repo.AddAsync(produto);

        return MapToResponse(created);
    }

    public async Task<IEnumerable<ProdutoResponseDto>> GetAllAsync()
    {
        var produtos = await _repo.GetAllAsync();

        return produtos.Select(MapToResponse);
    }

    public async Task<ProdutoResponseDto?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id inválido.");

        var produto = await _repo.GetByIdAsync(id);

        return produto == null ? null : MapToResponse(produto);
    }

    public async Task UpdateAsync(ProdutoUpdateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (dto.Id <= 0)
            throw new ArgumentException("Id inválido.");

        var produto = await _repo.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        if (!string.IsNullOrWhiteSpace(dto.Nome))
            produto.Nome = dto.Nome;

        produto.Descricao = dto.Descricao;
        produto.Preco = dto.Preco;
        produto.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(produto);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id inválido.");

        var produto = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        // Soft delete (recomendado)
        produto.Ativo = false;
        produto.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(produto);

        // 👉 Se preferir delete físico, substitua por:
        // await _repo.DeleteAsync(id);
    }

    private static ProdutoResponseDto MapToResponse(Produto p)
    {
        return new ProdutoResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            Ativo = p.Ativo,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        };
    }
}
