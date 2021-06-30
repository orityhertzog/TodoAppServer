using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Entities;

namespace TodoAppServer.Data
{
    public class TodoAppDataContext : DbContext
    {
        public DbSet<TodoList> TodoList { get; set;}
        public DbSet<TodoItem> TodoItem { get; set;}

        public TodoAppDataContext() { }
        public TodoAppDataContext(DbContextOptions<TodoAppDataContext> option) : base(option) { }
    }
}
