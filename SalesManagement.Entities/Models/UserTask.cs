using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Entities.Models
{
    public class UserTask
    {
        public int TaskId { get; set; }
        public TasksTask TasksTask { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
