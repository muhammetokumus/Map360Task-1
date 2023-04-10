using Map360Task.Application.Repositories;
using Map360Task.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Map360Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserReadRepository _readRepository;
        private readonly IUserWriteRepository _writeRepository;

        public UsersController(IUserReadRepository readRepository, IUserWriteRepository writeRepository)
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

        [HttpGet("WithCompanyAndRole")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersWithCompanyAndRole()
        {
            var users = await _readRepository.Table
                .Include(u => u.Company)
                .Include(u => u.Role)
                .ToListAsync();

            var userDtos = users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                Company = u.Company != null ? new Company
                {
                    Id = u.Company.Id,
                    Name = u.Company.Name
                } : null,
                Role = u.Role != null ? new Role
                {
                    Id = u.Role.Id,
                    Name = u.Role.Name
                } : null
            });

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id != null)
                return Ok(await _readRepository.GetByIdAsync(id));
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(User model)
        {
            if (ModelState.IsValid && model != null)
            {
                var nameJson = JsonConvert.SerializeObject(new { Name = model.Name });
                var surnameJson = JsonConvert.SerializeObject(new { Surname = model.Surname });
                var emailJson = JsonConvert.SerializeObject(new { Email = model.Email });
                var roleId = model.RoleId;
                var companyId = model.CompanyId;
                var newUser = new User()
                {
                    Name = nameJson,
                    Surname = surnameJson,
                    Email = emailJson,
                    RoleId = roleId,
                    CompanyId = companyId
                };
                await _writeRepository.AddAsync(newUser);
                await _writeRepository.SaveAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(User model)
        {
            if (ModelState.IsValid && model != null)
            {
                var nameJson = JsonConvert.SerializeObject(new { Name = model.Name });
                var surnameJson = JsonConvert.SerializeObject(new { Surname = model.Surname });
                var emailJson = JsonConvert.SerializeObject(new { Email = model.Email });
                model.Name = nameJson;
                model.Surname = surnameJson;
                model.Email = emailJson;
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
