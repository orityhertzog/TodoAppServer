using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppServer.Entities
{
    public record TodoItem(string Id, 
                            string ListId, 
                            string Caption, 
                            bool IsCompleted) { }



}
