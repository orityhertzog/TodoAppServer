using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Entities;

namespace TodoAppServer.Services
{

    public interface IRepositoryService
    {
        public Task<List<TodoList>> GetAllLists();
        public Task<List<TodoItem>> GetAllItems();
        public Task<TodoList> GetListById(string id);
        public Task<TodoItem> GetItemById(string id);
        public Task<TodoList> EditList(string id, TodoList list);
        public Task<TodoItem> EditItem(string id, TodoItem item);
        public Task<TodoList> DeleteList(string id);
        public Task<TodoItem> DeleteItem(string id);

        public Task<TodoList> AddNewList(TodoList list);
        public Task<TodoItem> AddNewItem(TodoItem item);
    }
}
