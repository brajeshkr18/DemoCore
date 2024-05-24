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
        public List<Order> GetAllActiveOrders()
        {
            return _context.Orders.AsNoTracking().Where(x=>!x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate).ToList();
        }
        public List<Order> GetRecentOrders()
        {
            var currentDate=DateTime.Now.Date;
            return _context.Orders.AsNoTracking().
                Where(x => !x.IsDeleted && x.CreatedDate.Date==currentDate)
               .OrderByDescending(x => x.CreatedDate).ToList();
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders
                              .Where(x => x.Id == id)
                              .AsNoTracking().FirstOrDefault();
        }

        public Order AddOrder(Order objOrder)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    _context.Orders.Add(objOrder);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException(ex.Message);
                }
            }
            return objOrder;
        }
    }
}
