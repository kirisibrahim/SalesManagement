// SalesManagement.Business/DTOs/TasksTaskDto.cs
using System;
using System.Collections.Generic;
using SalesManagement.Entities.Enums;
using SalesManagement.Entities.Models;

namespace SalesManagement.Business.DTOs
{
    public class TasksTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public Durum Durum { get; set; }
        public List<int> UserIds { get; set; } = new List<int>();
    }
}
