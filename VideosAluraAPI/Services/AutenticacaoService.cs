using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideosAluraAPI.Data;
using VideosAluraAPI.Data.Dtos;
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Services
{
    public class AutenticacaoService : IAuthorizationHandler
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly TokenService _tokenService;

        public AutenticacaoService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public AutenticacaoService()
        {
        }

        public async Task<string> Autenticar(LoginUsuarioDto usuarioDto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(usuarioDto.Username, usuarioDto.Password, false, false);
            if (!resultado.Succeeded) { throw new ApplicationException("Usuário não autenticadi. "); }

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == usuarioDto.Username.ToUpper());
            var token = _tokenService.GenerateToken(usuario);
            return token; 
        }

        public async Task Cadastrar(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            var resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário.");
            }
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}

