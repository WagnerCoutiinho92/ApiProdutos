using CrudApi.Application.DTOs;
using CrudApi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Api.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutosController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Post(ProdutoCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(ProdutoUpdateDto dto)
    {
        await _service.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}