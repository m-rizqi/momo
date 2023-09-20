using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.datasource.database.entity
{
    internal class TaskEntity
    {
        private String id;
        private String name;
        private String description;
        private bool isCompleted;

        public string Id { get => id; }
        public string Name { get => name; }
        public string Description { get => description; }
        public bool IsCompleted { get => isCompleted; }

        public Task(String id, String name, String description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.isCompleted = false;
        }
    }
}
