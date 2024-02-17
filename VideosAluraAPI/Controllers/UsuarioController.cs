using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideosAluraAPI.Data;
using VideosAluraAPI.Data.Dtos;
using VideosAluraAPI.Services;

namespace VideosAluraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AutenticacaoService _usuarioService;

        public UsuarioController(AutenticacaoService service)
        {
            _usuarioService = service;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            await _usuarioService.Cadastrar(usuarioDto);
            return Ok("Usuario cadastrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogarUsuario(LoginUsuarioDto usuarioDto)
        {
            var token = await _usuarioService.Autenticar(usuarioDto);
            return Ok(token);
        }


    }
}
