using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using Patrimonios.Domain.Queries.Patrimonios;
using Patrimonios.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Patrimonios.Api.Controllers.V1
{
    [Route("api/v1/patrimonios")]
    [Produces("application/json")]
    [ApiController]
    public class PatrimoniosController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        /// <response code="200"></response> 
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetAllPatrimoniosQueryResult>))]
        [HttpGet]
        public IActionResult Get([FromServices] IPatrimonioRepository repository)
        {
            var result = repository.GetAll()?.Select(x => (GetAllPatrimoniosQueryResult)x);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        /// <response code="200"></response> 
        [ProducesResponseType(200, Type = typeof(GetPatrimonioByIdQueryResult))]
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id, [FromServices] IPatrimonioRepository repository)
        {
            var result = (GetPatrimonioByIdQueryResult)repository.GetById(id);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        /// <response code="201"></response> 
        [ProducesResponseType(201, Type = typeof(SuccessCommandResult<CreatePatrimonioCommandResult>))]
        [ProducesResponseType(400, Type = typeof(ErrorCommandResult<CreatePatrimonioCommandResult>))]
        [HttpPost]
        public IActionResult Post([FromBody] CreatePatrimonioCommand command, [FromServices] IMediator mediator)
        {
            var result = mediator.Send(command).Result;

            if (result is SuccessCommandResult<CreatePatrimonioCommandResult>)
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
        [ProducesResponseType(200, Type = typeof(SuccessCommandResult<UpdatePatrimonioCommandResult>))]
        [ProducesResponseType(400, Type = typeof(ErrorCommandResult<UpdatePatrimonioCommandResult>))]
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpdatePatrimonioCommand command, [FromServices] IMediator mediator)
        {
            command.Id = id;
            var result = mediator.Send(command).Result;

            if (result is SuccessCommandResult<UpdatePatrimonioCommandResult>)
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
        [ProducesResponseType(200, Type = typeof(SuccessCommandResult<DeletePatrimonioCommand>))]
        [ProducesResponseType(400, Type = typeof(ErrorCommandResult<DeletePatrimonioCommand>))]
        [HttpDelete("{id}")]
        public IActionResult Put([FromRoute] Guid id, [FromServices] IMediator mediator)
        {
            DeletePatrimonioCommand command = new DeletePatrimonioCommand { Id = id };

            var result = mediator.Send(command).Result;

            if (result is SuccessCommandResult<DeletePatrimonioCommandResult>)
                return StatusCode((int)HttpStatusCode.OK, result);
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
    }
}
