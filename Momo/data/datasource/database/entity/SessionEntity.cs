using System;
using System.ComponentModel.DataAnnotations;

namespace Momo.data.datasource.database.entity
{
    public class SessionEntity
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int TimeId { get; set; }

        public int UserId { get; set; }

        public SessionEntity() { }

        public SessionEntity(int id, int taskId, int timeId, int userId)
        {
            Id = id;
            TaskId = taskId;
            TimeId = timeId;
            UserId = userId;
        }
    }
}
