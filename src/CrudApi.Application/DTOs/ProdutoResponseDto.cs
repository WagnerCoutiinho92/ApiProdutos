namespace CrudApi.Application.DTOs;

public record ProdutoResponseDto(int Id, string Nome, decimal Preco, bool Ativo);