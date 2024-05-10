using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            return await _context.Users
                  .Where(user => user.Orders.Any(order => order.Status == OrderStatus.Delivered && order.CreatedAt.Year == 2003))
                  .OrderByDescending(user => user.Orders.Sum(x => x.Quantity * x.Price))
                  .FirstOrDefaultAsync() ?? null!;

        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users
                .Where(user => user.Orders.Any(order => order.Status == OrderStatus.Paid && order.CreatedAt.Year == 2010))
                .ToListAsync();
        }
    }
}
