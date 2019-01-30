using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Dtos;
using MusicStoreApi.Models;
using MusicStoreApi.Repository.Interfaces;
using System;
using System.Linq;

namespace MusicStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var users = _userRepository.GetAll();

            if (!users.Any())
                return NotFound(new { success = false, data = "" });

            return Ok(new { success = true, data = users });
        }

        [HttpGet("{id}")]
        public IActionResult Find(Guid id)
        {
            var user = _userRepository.Find(id);

            if (user == null)
                return NotFound(new { success = false, data = "" });

            return Ok(new { success = true, data = user });
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(new { success = false, data = "" });

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };

            _userRepository.Create(user);

            return Created(new Uri($"{Request.Path}/{user.Id}", UriKind.Relative), new { success = true, data = user });
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UserDto userDto)
        {
            if (userDto == null || userDto.Id != id)
                return BadRequest(new { success = false, data = "" });

            var user = _userRepository.Find(id);

            if (user == null)
                return NotFound(new { success = false, data = "" });

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.DateUpdated = DateTime.Now;

            _userRepository.Update(user);

            return Ok(new { success = true, data = user });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _userRepository.Find(id);

            if (user == null)
                return NotFound(new { success = false, data = "" });

            _userRepository.Delete(id);

            return NotFound(new { success = true, data = "" });
        }
    }
}