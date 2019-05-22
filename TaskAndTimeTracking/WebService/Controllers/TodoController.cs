using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Controller.interfaces;

namespace TaskAndTimeTracking.WebService.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoController Controller;

        public TodoController(ITodoController controller)
        {
            Controller = controller;
        }
        
        /**
         * Get users assigned to this t0do
         */
        [Authorize, HttpGet("{id}/User")]
        public async Task<ActionResult<ResponseDTO<List<UserResponseDTO>>>> GetAssignedUsers(int id)
        {
            return new ResponseDTO<List<UserResponseDTO>>(await Controller.getAssignedUsers(id));
        }
        
        /**
         * Assign a user to 
         */
        [Authorize, HttpPut("{id}/User/{uid}")]
        public async Task<ActionResult<ResponseDTO<TodoDTO>>> AssignUser(int id, int uid)
        {
            string message = await Controller.assignUser(id, uid);
            if (String.IsNullOrEmpty(message))
            {
                return new ResponseDTO<TodoDTO>();
            }
            return new ResponseDTO<TodoDTO>(message);
        }
        
        /**
         * Remove a user assigned to this t0do
         */
        [Authorize, HttpDelete("{id}/User/{uid}")]
        public async Task<ActionResult<ResponseDTO<TodoDTO>>> RemoveUser(int id, int uid)
        {
            string message = await Controller.unassignUser(id, uid);
            if (String.IsNullOrEmpty(message))
            {
                return new ResponseDTO<TodoDTO>();
            }
            return new ResponseDTO<TodoDTO>(message);
        }
        
        
        [Authorize, HttpGet("{id}/WorkProgress")]
        public async Task<ActionResult<ResponseDTO<List<WorkProgressResponseDTO>>>> GetWorkProgress(int id)
        {
            return new ResponseDTO<List<WorkProgressResponseDTO>>(await Controller.getWorkProgress(id));
        }
        
        
        [Authorize, HttpPost("{id}/WorkProgress")]
        public async Task<ActionResult<ResponseDTO<WorkProgressResponseDTO>>> AddWorkProgress(
            int id, [FromBody] WorkProgressRequestDTO dto)
        {
            return new ResponseDTO<WorkProgressResponseDTO>(await Controller.addWorkProgress(id, dto));
        }
        
        
        [Authorize, HttpPut("{id}/WorkProgress")]
        public async Task<ActionResult<ResponseDTO<WorkProgressResponseDTO>>> UpdateWorkProgress(
            int id, [FromBody] WorkProgressRequestDTO dto)
        {
            throw new NotImplementedException();
        }
        
        
        [Authorize, HttpDelete("{id}/WorkProgress")]
        public async Task<ActionResult<ResponseDTO<WorkProgressResponseDTO>>> RemoveWorkProgress(
            int id, [FromBody] WorkProgressRequestDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}