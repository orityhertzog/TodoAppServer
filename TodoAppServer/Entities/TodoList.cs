using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppServer.Entities
{
    public record TodoList(string Id,
                        string Caption,
                        string Description,
                        string IconName,
                        string Color
                        )
    { }
}
