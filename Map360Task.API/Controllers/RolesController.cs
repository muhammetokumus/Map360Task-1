using Map360Task.Application.Repositories;
using Map360Task.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Map360Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleReadRepository _readRepository;
        private readonly IRoleWriteRepository _writeRepository;

        public RolesController(IRoleReadRepository readRepository, IRoleWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (_readRepository.GetAll() != null)
                return Ok(_readRepository.GetAll());
            return BadRequest();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id != null)
                return Ok(await _readRepository.GetByIdAsync(id));
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Role model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _writeRepository.AddAsync(model);
                await _writeRepository.SaveAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Role model)
        {
            if (ModelState.IsValid && model != null)
            {
                _writeRepository.Update(model);
                await _writeRepository.SaveAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var model = await _readRepository.GetByIdAsync(id);
                _writeRepository.Remove(model);
                await _writeRepository.SaveAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
