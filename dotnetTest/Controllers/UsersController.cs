using dotnetTest.Data;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnetTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get()
        {
            return Ok(await _userRepository.GetUsersAsync());
        }

        // GET api/<UsersController>/5
        [HttpGet("{iin}")]
        public async Task<ActionResult<AppUser>> Get(string iin)
        {
            var user = await _userRepository.GetUserByIinAsync(iin);
            if (user == null) return NotFound("Such user not found");
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<AppUser>> Post(AppUser user)
        {
            if (await _userRepository.UserExistsAsync(user.Iin))
                return BadRequest("This person already registered.");

            return Ok(await _userRepository.CreateUserAsync(user));
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<ActionResult> Put(AppUser user)
        {
            if (await _userRepository.GetUserByIdAsync(user.Id) == null)
                return NotFound("User wasn't found");

            await _userRepository.UpdateUserAsync(user);
            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User wasn't found");
            await _userRepository.DeleteUserAsync(user);
            return Ok();
        }
    }
}
