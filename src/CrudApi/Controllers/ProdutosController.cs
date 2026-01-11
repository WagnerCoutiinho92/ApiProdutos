using CrudApi.DTOs;
using CrudApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;
        public ProdutosController(IProdutoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.Listar());

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoCreateDto dto)
        {
            await _service.Criar(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProdutoUpdateDto dto)
        {
            await _service.Atualizar(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Excluir(id);
            return NoContent();
        }
    }
}