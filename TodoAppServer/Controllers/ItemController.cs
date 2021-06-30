using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAppServer.Entities;
using TodoAppServer.Services;



namespace TodoAppServer.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IRepositoryService _repo;

        public ItemController(IRepositoryService repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAllItems()
        {
            var res = await _repo.GetAllItems();
            return Ok(res);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetItemById(string id)
        {
            try
            {
                var res = await _repo.GetItemById(id);
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


        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post([FromBody] TodoItem item)
        {
            var itm = await _repo.AddNewItem(item);
            var newId = itm.Id;
            return Created($"~/items/{newId}", itm);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> Put(string id, [FromBody] TodoItem item)
        {
            try
            {
                var l = await _repo.EditItem(id, item);
                return Ok(l);
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


        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> Delete(string id)
        {
            try
            {
                var item = await _repo.DeleteItem(id);
                return Ok(item);
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
    }
}
