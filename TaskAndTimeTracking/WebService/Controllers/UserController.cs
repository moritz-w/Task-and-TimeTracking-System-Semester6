using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Controller.interfaces;

namespace TaskAndTimeTracking.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserController Controller;

        public UserController(IUserController controller)
        {
            Controller = controller;
        }

        [Authorize, HttpGet]
        public async Task<ActionResult<ResponseDTO<List<UserResponseDTO>>>> Get()
        {
            var users = await Controller.getAll();
            if (users == null)
            {
                return new EmptyResult();
            }
            return users;
        }
        
        [Authorize, HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<UserResponseDTO>>> Get (int id)
        {
            var user = await Controller.getById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [Authorize, HttpGet("me")]
        public async Task<ActionResult<ResponseDTO<UserResponseDTO>>> GetMe()
        {
            var identityName = Request.HttpContext.User.Identity.Name;
            return await Controller.getByEMail(identityName);
        }
        
        [Authorize, HttpPost("me")]
        public async Task<ActionResult<ResponseDTO<UserResponseDTO>>> PostMe()
        {
            throw new NotImplementedException();
        }

        [Authorize, HttpPost]
        public async Task<ResponseDTO<UserResponseDTO>> Post([FromBody] UserRequestDTO newUserRequest)
        {
            return await Controller.add(newUserRequest);
        }

        [Authorize, HttpPut]
        public async Task<ActionResult> Put([FromBody] UserRequestDTO userRequest)
        {
            await Controller.update(userRequest);
            return Ok();
        }
    }
}