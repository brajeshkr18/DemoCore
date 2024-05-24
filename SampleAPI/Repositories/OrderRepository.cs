using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;

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
            var currentDate = DateTime.Now.Date;
            return await _context.Orders.AsNoTracking().
                Where(x => !x.IsDeleted && x.CreatedDate.Date == currentDate)
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

        public async Task<bool> RemoveOrder(int id, int lastUpdatedBy)
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
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
