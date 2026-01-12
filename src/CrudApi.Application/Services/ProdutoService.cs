using AutoMapper;
using CrudApi.Application.DTOs;
using CrudApi.Application.Interfaces;
using CrudApi.Domain.Entities;
using CrudApi.Infrastructure.Interfaces;

namespace CrudApi.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoResponseDto>> Listar()
        => _mapper.Map<IEnumerable<ProdutoResponseDto>>(await _repository.Listar());

    public async Task Criar(ProdutoCreateDto dto)
    {
        var produto = _mapper.Map<Produto>(dto);
        produto.DataCadastro = DateTime.UtcNow;
        produto.Ativo = true;

        await _repository.Adicionar(produto);
    }

    public async Task Atualizar(ProdutoUpdateDto dto)
    {
        var produto = await _repository.ObterPorId(dto.Id);
        if (produto is null) return;

        _mapper.Map(dto, produto);
        await _repository.Atualizar(produto);
    }

    public Task Excluir(int id) => _repository.SoftDelete(id);
}