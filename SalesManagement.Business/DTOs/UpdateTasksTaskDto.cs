// SalesManagement.Business/DTOs/UpdateTasksTaskDto.cs
using System.Collections.Generic;
using SalesManagement.Entities.Enums;
using SalesManagement.Entities.Models;

namespace SalesManagement.Business.DTOs
{
    public class UpdateTasksTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Durum Durum { get; set; }
        // Güncelleme sırasında eklenmek istenen kullanıcı ID'leri
        public List<int> UserIds { get; set; } = new List<int>();
    }
}
