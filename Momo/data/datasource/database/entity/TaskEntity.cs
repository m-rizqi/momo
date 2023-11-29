using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.datasource.database.entity
{
    public class TaskEntity
    {
        private int id;
        private int userId;
        private String name;
        private String description;
        private bool isCompleted;

        public int TaskId { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public string Name { get => name; set => name = value; }
        public bool IsCompleted { get => isCompleted; set => isCompleted = value;}
        public string Description { get => description; set => description = value; }

        public TaskEntity() { }
        public TaskEntity(String name, String description)
        {
            Name = name;
            Description = description;
            IsCompleted = false;
        }
        public TaskEntity(int userId, String name, String description)
        {
            UserId = userId;
            Name = name;
            Description = description;
            IsCompleted = false;
        }

        public TaskEntity(int taskId, int userId, String name, String description)
        {
            TaskId = taskId;
            UserId = userId;
            Name = name;
            Description = description;
            IsCompleted = false;
        }
    }
}
