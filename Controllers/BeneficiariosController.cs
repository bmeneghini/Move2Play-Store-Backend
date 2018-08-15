using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.DTO;
using Api.Business;
using System.ServiceModel;
using Api.Shared;

namespace Api.Controllers
{
    /// <summary>
    /// Organiza as rotas da API para o modulo de Beneficiarios
    /// </summary>
    [Route("[controller]")]
    [Authorize]
    public class BeneficiariosController : ControllerBase
    {

        private readonly ILogger<BeneficiariosController> _logger;

        public BeneficiariosController(ILogger<BeneficiariosController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Efetua a verificação de elegibilidade e a solicitação do procedimento
        /// </summary>
        /// <param name="beneficiarioId">Id do beneficiário</param>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Beneficiário não encontrado</response>
        /// <response code="406">Parâmetros de entrada inválidos</response>
        /// <response code="500">Erro inesperado</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("{beneficiarioId:int}/autorizar")]
        public IActionResult AutorizarProcedimento(int? beneficiarioId, [FromBody] RequisicaoAutorizacaoDTO Requisicao)
        {
            //_logger.LogInformation("BeneficiariosController's method AutorizarProcedimento called");

            if (!beneficiarioId.HasValue || beneficiarioId <= 0)
            {
                return StatusCode(406, "Informe o beneficiário");
            }
            else if (Requisicao.CNPJPrestador == null || Requisicao.NomePrestador == null)
            {
                return StatusCode(406, "Informe o prestador");
            }

            if (Requisicao.NumeroCarteiraBeneficiario == null)
                Requisicao.NumeroCarteiraBeneficiario = beneficiarioId.Value.ToString();

            var retorno = new RetornoAutorizacaoDTO();

            try
            {
                var elegibilidadeBusiness = new ElegibilidadeBusiness();
                var retornoElegibilidade = new ElegibilidadeDTO();
                retornoElegibilidade = elegibilidadeBusiness.verificaElegibilidade(Requisicao);
                retorno.Elegibilidade = retornoElegibilidade;
            }
            catch (BusinessException e)
            {
                return StatusCode(406, e.Message);
            }

            if (retorno.Elegibilidade == null)
            {
                return NotFound("Beneficiário não encontrado");
            }

            Requisicao.RegistroANS = retorno.Elegibilidade.RegistroANS;
            Requisicao.NomeBeneficiario = retorno.Elegibilidade.NomeBeneficiario;

            try
            {
                var solicitacaoProcedimentoBusiness = new SolicitacaoProcedimentoBusiness();
                retorno.SolicitacaoProcedimento = solicitacaoProcedimentoBusiness.solicitarProcedimento(Requisicao);

            }
            catch (BusinessException e)
            {
                return StatusCode(406, e.Message);
            }

            if (retorno.SolicitacaoProcedimento == null)
            {
                return NotFound("Beneficiário não autorizado");
            }

            return Ok(retorno);
        }
    }
}