using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VideosAluraAPI.Data;
using VideosAluraAPI.Data.Dtos;
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{

    private readonly VideoDbContext _context;
    private readonly IMapper _mapper;

    public CategoriaController(VideoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public IEnumerable<ReadCategoriaDto> GetCategorias([FromQuery] int take = 5, [FromQuery] int skip = 1 ) 
    {
        return _mapper.Map<List<ReadCategoriaDto>>(_context.Categorias.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoriaById(int id) 
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
        if (categoria == null)
        {
            return NotFound();
        }
        var categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
        return Ok(categoriaDto);
    }

    [HttpGet("{categoriaId}/videos")]
    public IActionResult GetVideosPorCategoria(int categoriaId)
    {
        var categoria = _context.Categorias.Include(c => c.Videos).FirstOrDefault(c => c.Id == categoriaId);
        if (categoria == null) return NotFound("Categoria não encontrada");
        return Ok(categoria.Videos);
    }


    [HttpPost]
    public IActionResult CreateCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCategoriaById), new { id = categoria.Id }, categoria);

    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
        if (categoria == null) return NotFound();
        _mapper.Map(categoriaDto, categoria);
        _context.SaveChanges();
        return Ok(categoriaDto);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchCategoria(int id, JsonPatchDocument<UpdateCategoriaDto> patch)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
        if (categoria == null) return NotFound("Categoria não encontrada.");
        var categoriaPatch = _mapper.Map<UpdateCategoriaDto>(categoria);
        patch.ApplyTo(categoriaPatch, ModelState);
        if (!TryValidateModel(categoriaPatch))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(categoriaPatch, categoria);
        _context.SaveChanges();
        return Ok(categoria);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategoria(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
        if (categoria == null) return NotFound($"Categoria com id {id} não encontrada.");
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return Ok($"Categoria '{categoria}' removida com sucesso.");
    }
}
