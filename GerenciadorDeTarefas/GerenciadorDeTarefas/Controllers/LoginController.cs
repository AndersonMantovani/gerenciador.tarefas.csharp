using GerenciadorDeTarefas.Dtos;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GerenciadorDeTarefas.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class LoginController : ControllerBase
	{
		private readonly ILogger<LoginController> _logger;

		private readonly string loginTeste = "admin@admin.com";
		private readonly string senhaTeste = "Admin123456";

		public LoginController(ILogger<LoginController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
		{
			try
			{
				if (requisicao == null
					|| string.IsNullOrEmpty(requisicao.Login) || string.IsNullOrWhiteSpace(requisicao.Login)
					|| string.IsNullOrEmpty(requisicao.Senha) || string.IsNullOrWhiteSpace(requisicao.Senha)
					|| requisicao.Login != loginTeste || requisicao.Senha != senhaTeste)
				{
					return BadRequest(new ErroRespostaDto()
					{
						Status = StatusCodes.Status400BadRequest,
						Erro = "Parametros de entrada invalidos"
					});
				}

				var usuarioTeste = new Usuario()
				{
					Id = 1,
					Nome = "Usuario de Teste",
					Email = loginTeste,
					Senha = senhaTeste
				};

				var tokem = TokenService.CriarToken(usuarioTeste);

				return Ok(new LoginRespostaDto()
				{
					Email = usuarioTeste.Email,
					Nome = usuarioTeste.Nome,
					Token = tokem

				});
			}
			catch (Exception excecao)
			{
				_logger.LogError($"Ocorreu erro ao efetuar login: login = {excecao.Message}", excecao);
				return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostaDto()
				{
					Status = StatusCodes.Status500InternalServerError,
					Erro = "Ocorreu erro ao efetuar login, tente novamente!"
				});
			}

		}
	}
}
