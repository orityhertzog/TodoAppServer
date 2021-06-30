using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppServer.Entities
{
    public class TodoItem
    {
        public string Id { get; set;}
        public string ListId { get; set;}
        public string Caption { get; set;}
        public bool IsCompleted { get; set;}
        public TodoItem()
        {

        }
        public TodoItem(string id, string listid, string caption, bool iscompleted)
        {
            this.Id = id;
            this.ListId = listid;
            this.Caption = caption;
            this.IsCompleted = iscompleted;
        }
    }


}
