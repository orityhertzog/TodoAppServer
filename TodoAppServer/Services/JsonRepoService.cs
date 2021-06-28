using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Entities;


namespace TodoAppServer.Services
{

    public class JsonRepoService : IRepositoryService
    {
        private readonly string itemsUrl, listsUrl;
        private readonly string itemsPath, listsPath;

        public JsonRepoService(IConfiguration configuration) {
            this.itemsUrl = configuration["jsonItemsUrl"];   
            this.listsUrl = configuration["jsonListsUrl"];   
            this.itemsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", itemsUrl);
            this.listsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", listsUrl);
        }

        public async Task<TodoItem> AddNewItem(TodoItem item)
        {
            var allItems = await GetAllItems();
            var id = Guid.NewGuid().ToString();
            var newItem = new TodoItem(Id: id, ListId: item.ListId, Caption: item.Caption, IsCompleted: item.IsCompleted);
            allItems.Add(newItem);
            var objectsAsJson = JsonConvert.SerializeObject(allItems);
            await File.WriteAllTextAsync(itemsPath, objectsAsJson);
            return newItem;
        }

        public async Task<TodoList> AddNewList(TodoList list)
        {
            var allLists = await GetAllLists();
            var id = Guid.NewGuid().ToString();
            var newList = new TodoList(Id: id, Caption: list.Caption, Description: list.Description, IconName: list.IconName, Color: list.Color);
            allLists.Add(newList);
            var objectsAsJson = JsonConvert.SerializeObject(allLists);
            await File.WriteAllTextAsync(listsPath, objectsAsJson);
            return newList;
        }

        public async Task<TodoItem> DeleteItem(string id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("the id is null");
            }
            var allItems = await GetAllItems();
            var itemToDelete = allItems.Where(item => item.Id == id).SingleOrDefault();
            if(itemToDelete == null)
            {
                throw new ArgumentNullException("the object does not exist");
            }
            allItems.Remove(itemToDelete);
            var itemsAsJson = JsonConvert.SerializeObject(allItems);
            await File.WriteAllTextAsync(itemsPath, itemsAsJson);
            return itemToDelete;

        }

        public async Task<TodoList> DeleteList(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("the id is null");
            }
            var allLists = await GetAllLists();
            var specificList = allLists.Where(list => list.Id == id).SingleOrDefault();
            if (specificList == null)
            {
                throw new ArgumentNullException("the object does not exist");
            }
            var allItems = await GetAllItems();
            var allListItems = allItems.Where(item => item.ListId == specificList.Id);
            foreach (var item in allListItems)
            {
                await DeleteItem(item.Id);
            }
            allLists.Remove(specificList);
            var listsAsJson = JsonConvert.SerializeObject(allLists);
            await File.WriteAllTextAsync(listsPath, listsAsJson);
            return specificList;  
        }

        public async Task<TodoItem> EditItem(string id,TodoItem item)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("the id is null");
            }
            var allItems = await GetAllItems();
            var specificItem = allItems.Where(i => i.Id == item.Id).SingleOrDefault();
            if (specificItem == null)
            {
                throw new ArgumentNullException("the object does not exist");
            }
            allItems.Remove(specificItem);
            allItems.Add(item);
            var itemsAsJson = JsonConvert.SerializeObject(allItems);
            await File.WriteAllTextAsync(itemsPath, itemsAsJson);
            return item;
        }

        public async Task<TodoList> EditList(string id, TodoList list)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("the id is null");
            }
            var allLists = await GetAllLists();
            var specificList = allLists.Where(l => l.Id == list.Id).SingleOrDefault();
            if (specificList == null)
            {
                throw new ArgumentNullException("the object does not exist");
            }
            allLists.Remove(specificList);
            allLists.Add(list);
            var listsAsJson = JsonConvert.SerializeObject(allLists);
            await File.WriteAllTextAsync(listsPath, listsAsJson);

            return list;

        }

        public async Task<List<TodoItem>> GetAllItems()
        {
            var jsonString = await File.ReadAllTextAsync(itemsPath);
            var jsonAsObject = JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(jsonString);
            return jsonAsObject.ToList();
        }

        public async Task<List<TodoList>> GetAllLists()
        {
            var jsonString = await File.ReadAllTextAsync(listsPath);
            var jsonAsObject = JsonConvert.DeserializeObject<IEnumerable<TodoList>>(jsonString);
            return jsonAsObject.ToList();
        }

        public async Task<TodoItem> GetItemById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("the id is null");
            }
            var allItems = await GetAllItems();
            var specificItem = allItems.Where(item => item.Id.Equals(id)).DefaultIfEmpty(null).Single();
            if (specificItem == null)
            {
                throw new ArgumentNullException("the object does not exist");
            }

            return specificItem;
        }

        public async Task<TodoList> GetListById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("the id is null");
            }
            var allLists = await GetAllLists();
            var specificList = allLists.Where(lists => lists.Id.Equals(id)).DefaultIfEmpty(null).Single();
            if (specificList == null)
            {
                throw new ArgumentNullException("the object does not exist");
            }
            return specificList;
        }
    }
}
