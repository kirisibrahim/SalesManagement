using SalesManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
    }
}
