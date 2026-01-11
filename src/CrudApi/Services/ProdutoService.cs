using AutoMapper;
using CrudApi.Domain.Entities;
using CrudApi.DTOs;
using CrudApi.Repositories.Interfaces;
using CrudApi.Services.Interfaces;

namespace CrudApi.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoResponseDto>> Listar()
            => _mapper.Map<IEnumerable<ProdutoResponseDto>>(await _repo.Listar());

        public async Task Criar(ProdutoCreateDto dto)
        {
            var produto = _mapper.Map<Produto>(dto);
            produto.DataCadastro = DateTime.Now;
            produto.Ativo = true;
            await _repo.Adicionar(produto);
        }

        public async Task Atualizar(ProdutoUpdateDto dto)
        {
            var produto = await _repo.ObterPorId(dto.Id);
            if (produto == null) return;
            _mapper.Map(dto, produto);
            await _repo.Atualizar(produto);
        }

        public Task Excluir(int id) => _repo.SoftDelete(id);
    }
}