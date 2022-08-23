using CaseItau.API.Application.Handler.Fundo.Commands.Create;
using CaseItau.API.Application.Handler.Fundo.Commands.Delete;
using CaseItau.API.Application.Handler.Fundo.Commands.Update;
using CaseItau.API.Application.Handler.Fundo.Commands.UpdatePatrimonio;
using CaseItau.API.Application.Handler.Fundo.Queries.Find;
using CaseItau.API.Application.Handler.Fundo.Queries.GetAll;
using CaseItau.API.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseItau.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FundoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll", Name = "GetAll")]
        [ProducesResponseType(typeof(IEnumerable<FundoGetAllQueryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] FundoGetAllQueryRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [HttpGet("Find", Name = "Find")]
        [ProducesResponseType(typeof(FundoFindQueryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Find([FromQuery] FundoFindQueryRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [HttpPost]
        [ProducesResponseType(typeof(FundoCreateCommandResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] FundoCreateCommandRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [HttpPut]
        [ProducesResponseType(typeof(FundoUpdateCommandResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] FundoUpdateCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(FundoDeleteCommandResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromQuery] FundoDeleteCommandRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [HttpPut("patrimonio")]
        [ProducesResponseType(typeof(FundoPatrimonioUpdateCommandResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> MovimentarPatrimonio([FromBody] FundoPatrimonioUpdateCommandRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return Problem(ex.Message); }
        }
    }
}
