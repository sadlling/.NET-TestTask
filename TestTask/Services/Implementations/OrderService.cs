using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            return await _context.Orders
                .Where(order => order.Quantity > 1)
                .OrderByDescending(order => order.CreatedAt)
                .FirstOrDefaultAsync() ?? null!;

        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders
                 .Where(order => order.User.Status == UserStatus.Active)
                 .OrderByDescending(order => order.CreatedAt)
                 .ToListAsync();
        }
    }
}
