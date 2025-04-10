﻿using SalesManagement.Entities.Enums;
namespace SalesManagement.Entities.Models
{
    public class TasksTask
    {
        public int Id { get; set; }
        public string Title { get; set; }      // Görev başlığı
        public string Description { get; set; } // Görev açıklaması
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
        public Durum Durum { get; set; } = Durum.Pending;

    }
}
