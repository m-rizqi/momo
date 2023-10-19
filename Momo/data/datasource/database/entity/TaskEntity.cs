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

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool IsCompleted { get => isCompleted; set => isCompleted = value;}
        public string Description { get => description; set => description = value; }

        public TaskEntity() { }
        public TaskEntity(String id, String name, String description)
        {
            Id = id;
            Name = name;
            Description = description;
            IsCompleted = false;
        }
    }
}
