namespace CrudApi.Application.DTOs;

public record ProdutoUpdateDto(int Id, string Nome, decimal Preco, bool Ativo);