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

        public JsonRepoService(IConfiguration configuration)
        {
            this.itemsUrl = configuration["jsonItemsUrl"];
            this.listsUrl = configuration["jsonListsUrl"];
            this.itemsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", itemsUrl);
            this.listsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", listsUrl);
        }

        public async Task<TodoItem> AddNewItem(TodoItem item)
        {
            var allItems = await GetAllItems();
            var id = Guid.NewGuid().ToString();
            var newItem = new TodoItem(id, item.ListId, item.Caption, item.IsCompleted);
            allItems.Add(newItem);
            var objectsAsJson = JsonConvert.SerializeObject(allItems);
            await File.WriteAllTextAsync(itemsPath, objectsAsJson);
            return newItem;
        }

        public async Task<TodoList> AddNewList(TodoList list)
        {
            var allLists = await GetAllLists();
            var id = Guid.NewGuid().ToString();
            var newList = new TodoList(id, list.Caption, list.Description, list.IconName, list.Color);
            allLists.Add(newList);
            var objectsAsJson = JsonConvert.SerializeObject(allLists);
            await File.WriteAllTextAsync(listsPath, objectsAsJson);
            return newList;
        }

        public async Task<TodoItem> DeleteItem(string id)
        {
            IsIdNull(id);
            var allItems = await GetAllItems();
            var itemToDelete = allItems.Where(item => item.Id == id).SingleOrDefault();
            isObjectNull(id);
            allItems.Remove(itemToDelete);
            var itemsAsJson = JsonConvert.SerializeObject(allItems);
            await File.WriteAllTextAsync(itemsPath, itemsAsJson);
            return itemToDelete;

        }

        public async Task<TodoList> DeleteList(string id)
        {
            IsIdNull(id);
            var allLists = await GetAllLists();
            var specificList = allLists.Where(list => list.Id == id).SingleOrDefault();
            isObjectNull(specificList);
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

        public async Task<TodoItem> EditItem(string id, TodoItem item)
        {
            IsIdNull(id);
            var allItems = await GetAllItems();
            var specificItem = allItems.Where(i => i.Id == item.Id).SingleOrDefault();
            isObjectNull(specificItem);
            allItems.Remove(specificItem);
            allItems.Add(item);
            var itemsAsJson = JsonConvert.SerializeObject(allItems);
            await File.WriteAllTextAsync(itemsPath, itemsAsJson);
            return item;
        }

        public async Task<TodoList> EditList(string id, TodoList list)
        {
            IsIdNull(id);
            var allLists = await GetAllLists();
            var specificList = allLists.Where(l => l.Id == list.Id).SingleOrDefault();
            isObjectNull(specificList);
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
            IsIdNull(id);
            var allItems = await GetAllItems();
            var specificItem = allItems.Where(item => item.Id.Equals(id)).DefaultIfEmpty(null).Single();
            isObjectNull(specificItem);
            return specificItem;
        }

        public async Task<TodoList> GetListById(string id)
        {
            IsIdNull(id);
            var allLists = await GetAllLists();
            var specificList = allLists.Where(lists => lists.Id.Equals(id)).DefaultIfEmpty(null).Single();
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
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("the id is null");
            }
        }
    }
}
