﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.datasource.database.entity
{
    public class TaskEntity
    {
        private int id;
        private String name;
        private String description;
        private bool isCompleted;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool IsCompleted { get => isCompleted; set => isCompleted = value;}
        public string Description { get => description; set => description = value; }

        public TaskEntity() { }
        public TaskEntity(int id, String name, String description)
        {
            Id = id;
            Name = name;
            Description = description;
            IsCompleted = false;
        }
    }
}
