using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Marcas;
using Patrimonios.Domain.Queries.Marcas;
using Patrimonios.Domain.Queries.Patrimonios;
using Patrimonios.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Patrimonios.Api.Controllers.V1
{
    [Route("api/v1/marcas")]
    [Produces("application/json")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        /// <response code="200">Retorna todas as marcas cadastras</response> 
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetAllMarcasQueryResult>))]
        [HttpGet]
        public IActionResult Get([FromServices] IMarcaRepository repository)
        {

            var result = repository.GetAll()?.Select(x => (GetAllMarcasQueryResult)x);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        /// <response code="200"></response> 
        [ProducesResponseType(200, Type = typeof(GetMarcaByIdQueryResult))]
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id, [FromServices] IMarcaRepository repository)
        {
            var result = (GetMarcaByIdQueryResult)repository.GetById(id);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        /// <response code="200"></response> 
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetAllPatrimoniosFromMarcaIdQueryResult>))]
        [HttpGet("{id}/patrimonios")]
        public IActionResult GetPatrimoniosById([FromRoute] Guid id, [FromServices] IPatrimonioRepository repository)
        {
            var result = repository.GetAllFromMarcaId(id)?.Select(x => (GetAllPatrimoniosFromMarcaIdQueryResult)x);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        /// <response code="201"></response> 
        [ProducesResponseType(201, Type = typeof(SuccessCommandResult<CreateMarcaCommandResult>))]
        [ProducesResponseType(400, Type = typeof(ErrorCommandResult<CreateMarcaCommandResult>))]
        [HttpPost]
        public IActionResult Post([FromBody] CreateMarcaCommand command, [FromServices] IMediator mediator)
        {
            var result = mediator.Send(command).Result;

            if (result is SuccessCommandResult<CreateMarcaCommandResult>)
                return StatusCode((int)HttpStatusCode.Created, result);
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        /// <response code="200"></response> 
        [ProducesResponseType(200, Type = typeof(SuccessCommandResult<UpdateMarcaCommandResult>))]
        [ProducesResponseType(400, Type = typeof(ErrorCommandResult<UpdateMarcaCommandResult>))]
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpdateMarcaCommand command, [FromServices] IMediator mediator)
        {
            command.Id = id;
            var result = mediator.Send(command).Result;

            if (result is SuccessCommandResult<UpdateMarcaCommandResult>)
                return StatusCode((int)HttpStatusCode.OK, result);
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        /// <response code="200"></response> 
        [ProducesResponseType(200, Type = typeof(SuccessCommandResult<UpdateMarcaCommandResult>))]
        [ProducesResponseType(400, Type = typeof(ErrorCommandResult<UpdateMarcaCommandResult>))]
        [HttpDelete("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromServices] IMediator mediator)
        {
            DeleteMarcaCommand command = new DeleteMarcaCommand { Id = id };

            var result = mediator.Send(command).Result;

            if (result is SuccessCommandResult<DeleteMarcaCommandResult>)
                return StatusCode((int)HttpStatusCode.OK, result);
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
    }
}
