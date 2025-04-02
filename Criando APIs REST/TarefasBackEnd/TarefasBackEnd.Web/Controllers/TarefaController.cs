using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TarefasBackEnd.Web.Models;
using TarefasBackEnd.Web.Repository;

namespace TarefasBackEnd.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromServices] ITarefaRepository repository)
        {
            var id = new Guid(User.Identity.Name);
            var result = repository.Read(id);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            repository.Create(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Modify(string id, [FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {

            if (!ModelState.IsValid)
                return BadRequest();


            repository.Update(new Guid(id), model);

            return Ok();
        }


        [HttpDelete("{id}")]

        public IActionResult Delete(string id, [FromServices] ITarefaRepository repository)
        {
            repository.Delete(new Guid(id));
            return Ok();
        }
    }
}
