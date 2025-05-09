using Microsoft.AspNetCore.Mvc;
using todo_dotnet_core9.Applications.Interfaces;
using todo_dotnet_core9.Applications.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo_dotnet_core9.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskAppService _service;

        public TaskController(ITaskAppService service)
        {
            _service = service;
        }

        // GET: api/<TaskController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        // POST api/<TaskController>
        [HttpPost]
        public IActionResult Post([FromBody] TaskViewModel model)
        {
            _service.Add(model);
            return Ok();
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TaskViewModel model)
        {
            _service.Update(id, model);
            return Ok();
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
