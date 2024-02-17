using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideosAluraAPI.Data;
using VideosAluraAPI.Data.Dtos;
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VideoController : ControllerBase
{

    private readonly VideoDbContext _context;
    private readonly IMapper _mapper;

    public VideoController(VideoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddVideo(
        [FromBody] CreateVideoDto videoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (videoDto.CategoriaId == null)
        {
            var categoriaLivreId = _context.Categorias.FirstOrDefault(c => c.Id == 1);
            var categoriaLivreTitulo = _context.Categorias.FirstOrDefault(c => c.Titulo == "Livre");
            videoDto.CategoriaId = categoriaLivreId.Id;
        }

        var video = _mapper.Map<Video>(videoDto);
        _context.Videos.Add(video);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetVideoById),
            new { id = video.Id, video });
    }

    [HttpGet]
    public IEnumerable<ReadVideoDto> GetVideo([FromQuery] int? categoriaId = null, [FromQuery] string? titulo = null, [FromQuery] int skip = 1, [FromQuery] int take = 5)
    {
        IQueryable<Video> query = _context.Videos;
        if(categoriaId != null)
        {
            query = query.Where(video => video.CategoriaId == categoriaId);
        }
        if (!string.IsNullOrEmpty(titulo))
        {
            query = query.Where(video => video.Titulo.Contains(titulo));
        }

        return _mapper.Map<List<ReadVideoDto>>(query.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetVideoById(int id)
    {
        var video = _context.Videos
            .FirstOrDefault(video => video.Id == id);
        if (video == null) return NotFound();
        var videoDto = _mapper.Map<ReadVideoDto>(video);
        return Ok(videoDto);
    }

    [HttpGet("free")]
    public IEnumerable<ReadVideoDto> GetVideosGratuitos()
    {
        var videosGratuitos = _context.Videos.Where(v => v.CategoriaId == 1).ToList();
        return _mapper.Map<List<ReadVideoDto>>(videosGratuitos);

    }

    [HttpPut("{id}")]
    public IActionResult UpdateVideo(int id,
        [FromBody] UpdateVideoDto videoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var video = _context.Videos.FirstOrDefault(
            video => video.Id == id);
        if (video == null) return NotFound();
        _mapper.Map(videoDto, video);
        _context.SaveChanges();
        return Ok(videoDto);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchVideo(int id,
        JsonPatchDocument<UpdateVideoDto> patch)
    {
        var video = _context.Videos.FirstOrDefault(
            video => video.Id == id);
        if (video == null) return NotFound($"Video com id {id} não encontrado.");

        var videoParaAtualizar = _mapper.Map<UpdateVideoDto>(video);

        patch.ApplyTo(videoParaAtualizar, ModelState);

        if (!TryValidateModel(videoParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(videoParaAtualizar, video);
        _context.SaveChanges();
        return Ok(videoParaAtualizar);
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteVideo(int id)
    {
        var video = _context.Videos.FirstOrDefault(
            video => video.Id == id);
        if (video == null) return NotFound($"Video com id {id} não encontrado.");
        _context.Videos.Remove(video);
        _context.SaveChanges();
        return Ok($"Video '{video.Titulo}' removido com sucesso.");
    }
}
