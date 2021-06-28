using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Entities;
using TodoAppServer.Services;

namespace TodoAppServer.Controllers
{
    [Route("lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private IRepositoryService _repo;

        public ListController(IRepositoryService repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoList>>> GetAllLists()
        {
            var res = await _repo.GetAllLists();
            return Ok(res);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetListById(string id)
        {
            try
            {
                var res = await _repo.GetListById(id);
                return Ok(res);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);

            }
            catch (Exception e)
            {
                
                return BadRequest(e.Message);
            }
}


        [HttpPost()]
        public async Task<ActionResult<TodoList>> Post([FromBody] TodoList list)
        {
           var l = await _repo.AddNewList(list);
            var newId = l.Id;
            return Created($"~/lists/{newId}", l);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<TodoList>> Put(string id, [FromBody] TodoList list)
        {
            try
            {
                var l = await _repo.EditList(id, list);
                return Ok(l);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoList>> Delete(string id)
        {
            try
            {
                var list = await _repo.DeleteList(id);
                return Ok(list);
            }            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
