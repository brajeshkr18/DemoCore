using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using SampleAPI.Entities;
using SampleAPI.Helper;

namespace SampleAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected SampleApiDbContext _context;
        public OrderRepository(SampleApiDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllActiveOrders()
        {
            return await _context.Orders.AsNoTracking().Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate).ToListAsync();
        }
        public async Task<List<Order>> GetRecentOrders()
        {
            var recentOrdersDate = DateTime.Now.Date.AddDays(-1).Date;
            return await _context.Orders.AsNoTracking().
                Where(x => !x.IsDeleted && x.CreatedDate.Date == recentOrdersDate)
               .OrderByDescending(x => x.CreatedDate).ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders
                              .Where(x => x.Id == id)
                              .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Order> AddOrder(Order objOrder)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Orders.AddAsync(objOrder);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException(ex.Message);
                }
            }
            return objOrder;
        }

        public async Task<bool> DeleteOrder(int id, int lastUpdatedBy)
        {
            try
            {
                var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
                if (existingOrder != null)
                {
                    existingOrder.IsDeleted = true;
                    existingOrder.LastUpdatedBy = lastUpdatedBy;
                    existingOrder.LastUpdateDate = DateTime.Now;
                    _context.Orders.Update(existingOrder);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
           
        }

        public async Task<List<Order>> GetRecentOrdersByDays(int numberOfDays)
        {
            var startDate = DateTime.Today.AddDays(-numberOfDays);
            var endDate = DateTime.Today;

            // Adjust startDate to include non-working days
            while (startDate <= endDate)
            {
                if (DateHelper.IsWeekend(startDate.DayOfWeek))
                    startDate = startDate.AddDays(1);
                else
                    break;
            }

            // Query orders within the adjusted date range
            var orders = _context.Orders.Where(o => !o.IsDeleted && o.CreatedDate >= startDate && o.CreatedDate <= endDate).ToListAsync();
            return await orders;
        }

    }
}
