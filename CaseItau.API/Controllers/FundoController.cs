using System.Collections.Generic;
using CaseItau.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using AutoMapper;
using CaseItau.API.Business.Services;
using CaseItau.API.DTOs;
using CaseItau.API.Models;
using System.Threading.Tasks;

namespace CaseItau.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class FundoController : ControllerBase
    {
        #region Atributos
        private readonly IMapper _mapper;        
        private readonly FundoService _fundoService;
        #endregion

        #region Construtor
        public FundoController(IMapper mapper, FundoService fundoService)
        {
            _mapper = mapper;            
            _fundoService = fundoService;
        }
        #endregion

        #region Método Get        
        [HttpGet("obter-todos-fundos")]
        public async Task<IActionResult> ObterTodosFundos()
        {
            try
            {
                var fundos = _mapper.Map<List<FundoDTO>>(await _fundoService.ObterTodosFundos());
                if(fundos == null)
                {
                    return NotFound();
                }
                var retornoAPI = new RetornoPadraoAPI
                {
                    Dados = fundos
                };

                return Ok(retornoAPI);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, "", 500);
            }
        }

        [HttpGet("obter-fundo-por-codigo/{codigo}")]
        public async Task<IActionResult> ObterFundoPorCodigo(string codigo)
        {
            try
            {
                var fundo = _mapper.Map<List<FundoDTO>>(await _fundoService.ObterFundoPorCodigo(codigo));
                if (fundo == null)
                {
                    return NotFound();
                }
                var retornoAPI = new RetornoPadraoAPI
                {
                    Dados = fundo
                };

                return Ok(retornoAPI);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500);
            }
        }
        #endregion

        #region Método Post
        [HttpPost("criar-fundo")]
        public async Task<IActionResult> CriarFundo([FromBody] Fundo value)
        {
            try
            {
                var fundo = await _fundoService.CriarFundo(value);

                var retornoAPI = new RetornoPadraoAPI
                {
                    Dados = fundo,
                    Message = "Fundo Incluído com Sucesso"
                };

                return Ok(retornoAPI);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500);
            }
        }
        #endregion

        #region Método Put     
        [HttpPut("movimentar-patrimonio/{codigo}/{patrimonio}")]
        public async Task<IActionResult> MovimentarPatrimonio(string codigo, decimal? patrimonio)
        {
            try
            {
                var fundo = await _fundoService.MovimentarFundo(codigo, patrimonio);

                var retornoAPI = new RetornoPadraoAPI
                {
                    Dados = fundo,
                    Message = "Fundo Movimentado com Sucesso"
                };

                return Ok(retornoAPI);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500);
            }
        }

        [HttpPut("alterar-fundo/{codigo}")]
        public async Task<IActionResult> AlterarFundo(string codigo, [FromBody] Fundo value)
        {
            try
            {
                var fundo = await _fundoService.AlterarFundo(codigo, value);

                var retornoAPI = new RetornoPadraoAPI
                {
                    Dados = fundo,
                    Message = "Fundo Alterado com Sucesso"
                };

                return Ok(retornoAPI);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500);
            }
        }
        #endregion

        #region Método Delete 
        [HttpDelete("excluir-fundo/{codigo}")]
        public async Task<IActionResult> ExcluirFundo(string codigo)
        {
            try
            {
                var fundo = await _fundoService.ExcluirFundo(codigo);

                var retornoAPI = new RetornoPadraoAPI
                {
                    Dados = fundo,
                    Message = "Fundo Excluído com Sucesso"
                };

                return Ok(retornoAPI);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, "", 500);
            }
        }        
        #endregion
    }
}