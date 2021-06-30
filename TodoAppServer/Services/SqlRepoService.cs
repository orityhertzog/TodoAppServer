using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Data;
using TodoAppServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace TodoAppServer.Services
{
    public class SqlRepoService :IRepositoryService 
    {
        private readonly TodoAppDataContext _dataContext;

        public SqlRepoService(TodoAppDataContext dataContext)
        {
            this._dataContext = dataContext;

        }

        public async Task<TodoItem> AddNewItem(TodoItem item)
        {
            isObjectNull(item);
            var id = Guid.NewGuid().ToString();
            item.Id = id;
            await _dataContext.TodoItem.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
            
        }

        public async Task<TodoList> AddNewList(TodoList list)
        {
            isObjectNull(list);
            var id = Guid.NewGuid().ToString();
            list.Id = id;
            await _dataContext.TodoList.AddAsync(list);
            await _dataContext.SaveChangesAsync();
            return list;
        }

        public async Task<TodoItem> DeleteItem(string id)
        {
            IsIdNull(id);
            var item = await _dataContext.TodoItem.Where(i => i.Id == id).ToListAsync();
            var specificItem = item.SingleOrDefault();
            isObjectNull(specificItem);
            _dataContext.TodoItem.Remove(specificItem);
            await _dataContext.SaveChangesAsync();
            return specificItem;
        }

        public  async Task<TodoList> DeleteList(string id)
        {
            IsIdNull(id);
            var list = await _dataContext.TodoList.Where(l => l.Id == id).ToListAsync();
            var specificList = list.SingleOrDefault();
            isObjectNull(specificList);
            var items = await GetAllItems();
            var listItems = items.Where(i => i.ListId == id);
            _dataContext.TodoItem.RemoveRange(listItems);
            _dataContext.TodoList.Remove(specificList);
            await _dataContext.SaveChangesAsync();
            return specificList;
        }

        public async Task<TodoItem> EditItem(string id, TodoItem item)
        {
            isObjectNull(item);
            IsIdNull(id);
            var itm = await GetItemById(id);
            itm.IsCompleted = item.IsCompleted;
            itm.Caption = item.Caption;
            await _dataContext.SaveChangesAsync();
            return itm;
        }

        public async Task<TodoList> EditList(string id, TodoList list)
        {
            isObjectNull(list);
            IsIdNull(id);
            var lst = await GetListById(id);
            lst.Caption = list.Caption;
            lst.Color = lst.Color;
            lst.Description = list.Description;
            lst.IconName = list.IconName;

            await _dataContext.SaveChangesAsync();
            return lst;
        }

        public async Task<List<TodoItem>> GetAllItems()
        {
            var items = await _dataContext.TodoItem.ToListAsync();
            return items;
        }

        public async Task<List<TodoList>> GetAllLists()
        {
            var lists = await _dataContext.TodoList.ToListAsync();
            return lists;

        }

        public async Task<TodoItem> GetItemById(string id)
        {
            IsIdNull(id);
            var item = await _dataContext.TodoItem.Where(i => i.Id == id).ToListAsync();
            var specificItem = item.SingleOrDefault();
            isObjectNull(specificItem);
            return specificItem;
        }

        public async Task<TodoList> GetListById(string id)
        {
            IsIdNull(id);
            var list = await _dataContext.TodoList.Where(l => l.Id == id).ToListAsync();
            var specificList = list.SingleOrDefault();
            isObjectNull(specificList);
            return specificList;

        }

        private void isObjectNull(object specificObj)
        {
            if (specificObj == null)
                throw new ArgumentNullException("the object does not exist");
        }

        private void IsIdNull(string id)
        {
            if(string.IsNullOrEmpty(id))
                throw new ArgumentNullException("the id is null");
        }
    }
}
