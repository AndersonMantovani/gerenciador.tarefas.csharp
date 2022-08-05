using GerenciadorDeTarefas.Dtos;
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

		public LoginController(ILogger<LoginController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
		{
			try
			{
				if(requisicao == null || requisicao.Login == null || requisicao.Senha == null)
				{
					return BadRequest(new ErroRespostaDto()
					{
						Status = StatusCodes.Status400BadRequest,
						Erro = "Parametros de entrada invalidos"
					});
				}
               return Ok("Usuario autenticado com sucesso");
			}
			catch(Exception excecao)
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
