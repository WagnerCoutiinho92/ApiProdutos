using CrudApi.Application.DTOs;

namespace CrudApi.Application.Interfaces.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoResponseDto>> GetAllAsync();
    Task<ProdutoResponseDto?> GetByIdAsync(int id);
    Task<ProdutoResponseDto> CreateAsync(ProdutoCreateDto dto);
    Task UpdateAsync(ProdutoUpdateDto dto);
    Task DeleteAsync(int id);
}
