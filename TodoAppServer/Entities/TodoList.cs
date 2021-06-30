using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppServer.Entities {
    public class TodoList
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
        public string Color { get; set; }

        public TodoList()
        {

        }
        public TodoList(string caption, string desc, string icon, string color) 
        {
           
            this.Caption = caption;
            this.Description = desc;
            this.IconName = icon;
            this.Color = color;
        }

        public TodoList(string id, string caption, string desc, string icon, string color)
        {
            this.Id = id;
            this.Caption = caption;
            this.Description = desc;
            this.IconName = icon;
            this.Color = color;
        }
    }                   
}
