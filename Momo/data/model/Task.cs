using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.model
{
    class Task
    {
        String id;
        String name;
        String description;
        bool isCompleted;

        Task(String id, String name, String description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.isCompleted = false;
        }
    }
}
