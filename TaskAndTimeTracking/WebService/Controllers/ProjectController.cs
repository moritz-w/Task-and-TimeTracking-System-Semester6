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
    public class ProjectController : ControllerBase
    {
        private IProjectController Controller;

        public ProjectController(IProjectController controller)
        {
            Controller = controller;
        }

        [Authorize, HttpGet]
        public async Task<ActionResult<ResponseDTO<List<ProjectResponseDTO>>>> Get()
        {
            var projects = await Controller.getAll();
            if (projects == null)
            {
                return new EmptyResult();
            }
            return projects;
        }

        [Authorize, HttpPost]
        public async Task<ActionResult<ResponseDTO<ProjectResponseDTO>>> Post([FromBody] ProjectRequestDTO project)
        {
            return await Controller.add(project);
        }

        [Authorize, HttpPut]
        public async Task<ActionResult<ResponseDTO<ProjectResponseDTO>>> Put([FromBody] ProjectRequestDTO project)
        {
            return await Controller.update(project);
        }
        
        [Authorize, HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<ProjectResponseDTO>>> GetById(int id)
        {
            return await Controller.getById(id);
        }

        [Authorize, HttpGet("{id}/Todo")]
        public async Task<ActionResult<ResponseDTO<List<TodoDTO>>>> GetAssignedTodos(int id)
        {
            return new ResponseDTO<List<TodoDTO>>(await Controller.getAssignedTodos(id));
        }
        
        /**
         * Creates a new t0do item and assigns it to the corresponding project 
         */
        [Authorize, HttpPost("{id}/Todo")]
        public async Task<ActionResult<ResponseDTO<TodoDTO>>> CreateAndAssignTodo(int id, [FromBody] TodoDTO todo)
        {
            var newTodo = await Controller.createAndAssignTodo(id, todo);
            if (newTodo == null)
            {
                return new ResponseDTO<TodoDTO>("Assignment of TODO failed");
            }
            return new ResponseDTO<TodoDTO>(newTodo);
        }
        
        /**
         * Assigns an existing t0do item to an existing project
         */
        [Authorize, HttpPut("{id}/Todo")]
        public async Task<ActionResult<ResponseDTO<TodoDTO>>> AssignTodo(int id, [FromBody] int todoId)
        {
            throw new NotImplementedException();
        }

        [Authorize, HttpDelete("{id}/Todo/{tid}")]
        public async Task<ActionResult<ResponseDTO<TodoDTO>>> RemoveTodo(int id, int tid)
        {
            if (!await Controller.unassignTodo(id, tid))
            {
                return new ResponseDTO<TodoDTO>("Failed to delete TOOD");
            }

            return new ResponseDTO<TodoDTO>();
        }

        /**
         * Get a list of users assigned to the corresponding project 
         */
        [Authorize, HttpGet("{id}/User")]
        public async Task<ActionResult<ResponseDTO<List<UserResponseDTO>>>> GetAssignedUsers(int id)
        {
            return new ResponseDTO<List<UserResponseDTO>>(await Controller.getAssignedUsers(id));
        }

        [Authorize, HttpPut("{id}/User/{uid}")]
        public async Task<ActionResult<ResponseDTO<UserResponseDTO>>> AssignUser(int id, int uid)
        {
            if (!await Controller.assignUser(id, uid))
            {
                return new ResponseDTO<UserResponseDTO>("Failed to assign user to project");
            }
            return new ResponseDTO<UserResponseDTO>();
        }
        
        
        [Authorize, HttpDelete("{id}/User/{uid}")]
        public async Task<ActionResult<ResponseDTO<UserResponseDTO>>> RemoveUser(int id, int uid)
        {
            if (!await Controller.unassignUser(id, uid))
            {
                return new ResponseDTO<UserResponseDTO>("Failed to assign user to project");
            }
            return new ResponseDTO<UserResponseDTO>();
        }

        [Authorize, HttpGet("{id}/WorkReport")]
        public async Task<ActionResult<ResponseDTO<Object>>> GetWorkReport(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}