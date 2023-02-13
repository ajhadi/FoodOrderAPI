using FoodOrderAPI.Models.DTOs;
using static Constants;

namespace FoodOrderAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext context;
        private readonly ILogger<OrderService> logger;
        public OrderService(DataContext context,
        ILogger<OrderService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<ServiceStatus> CreateOrder(OrderDTO request, Guid userId)
        {
            try
            {
                var table = await context.Tables.FindAsync(request.TableId);
                if (!table.IsReady)
                    return ServiceStatus.ErrorResult(AppError.TableIsNotAvailable);
                var newOrder = new Order
                {
                    CustomerName = request.CustomerName,
                    IsPaid = request.IsPaid,
                    Status = Models.Enums.OrderStatus.Active,
                    TableId = request.TableId
                };
                await context.Orders.AddAsync(newOrder);
                if (request.Items is not null)
                {
                    newOrder.OrderItems = new List<OrderItem>();
                    int price = 0;
                    foreach (var item in request.Items)
                    {
                        var i = await context.Items.FindAsync(item.ItemId);
                        if (i is null)
                            return ServiceStatus.ErrorResult(AppError.ItemIsNotFound.AddMessage($"Id: {item.ItemId}"));
                        newOrder.OrderItems.Add(new OrderItem { ItemId = item.ItemId, ProductName = i.Name, Quantity = item.Quantity });
                        price = price + (i.Price * item.Quantity);
                    }
                    newOrder.Total = price;
                }
                table.IsReady = false;
                await context.SaveChangesAsync();

                await context.OrderActivities.AddAsync(new OrderActivity
                {
                    OrderId = newOrder.Id,
                    EmployeeId = userId,
                    Description = "Create new item"
                });
                await context.SaveChangesAsync();

                return ServiceStatus.SuccessResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult();
            }

        }
        public async Task<ServiceStatus<List<Order>>> GetOrders(OrderQueryDTO param, Guid userId)
        {
            try
            {
                var query = await context.Orders.ToListAsync();
                if (param.Status is not null)
                    query = query.Where(o => o.Status == param.Status).ToList();

                return ServiceStatus.SuccessObjectResult<List<Order>>(query);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<List<Order>>();
            }
        }
        public async Task<ServiceStatus<Order>> GetOrder(Guid id)
        {
            try
            {
                var query = await context.Orders.Include(o => o.OrderItems)
                .Where(o => o.Id == id).SingleOrDefaultAsync();
                if (query is null)
                    return ServiceStatus.ErrorResult<Order>(Constants.AppError.OrderIsNotFound);

                return ServiceStatus.SuccessObjectResult<Order>(query);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<Order>();
            }
        }
        public async Task<ServiceStatus<Order>> GetActiveOrder(int tableId)
        {
            try
            {
                var query = await context.Orders.Include(o => o.OrderItems)
                .Where(o => o.TableId == tableId && o.Status == Models.Enums.OrderStatus.Active).SingleOrDefaultAsync();

                if (query is null)
                    return ServiceStatus.ErrorResult<Order>(Constants.AppError.OrderIsNotFound);

                return ServiceStatus.SuccessObjectResult<Order>(query);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<Order>();
            }
        }
        public async Task<ServiceStatus<List<Order>>> GetOrderActivities(Guid userId)
        {
            try
            {
                var query = await context.Orders
                    .AsNoTracking()
                    .Include(o => o.Activities)
                    .Where(o => o.Activities.Any(a => a.EmployeeId == userId))
                    .Select(o => new Order
                    {
                        Id = o.Id,
                        Activities = o.Activities.Where(a => a.EmployeeId == userId).ToList(),
                        OrderNumber = o.OrderNumber,
                        CreatedDateUTC = o.CreatedDateUTC,
                        CreatedBy = o.CreatedBy,
                        IsDeleted = o.IsDeleted,
                        CustomerName = o.CustomerName,
                        Total = o.Total,
                        IsPaid = o.IsPaid,
                        Status = o.Status,
                    })
                    .ToListAsync();

                return ServiceStatus.SuccessObjectResult<List<Order>>(query);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<List<Order>>();
            }
        }

        public async Task<ServiceStatus> UpdateOrder(Guid id, OrderDetailDTO request, Guid userId)
        {
            try
            {
                var query = await context.Orders.Where(o => o.Id == id).SingleOrDefaultAsync();
                if (query is null)
                    return ServiceStatus.ErrorResult(Constants.AppError.OrderIsNotFound);

                query.CustomerName = request.CustomerName;
                query.IsPaid = request.IsPaid;
                query.TableId = request.TableId;
                query.Status = request.Status;
                await context.SaveChangesAsync();

                var table = await context.Tables.FindAsync(request.TableId);
                table.IsReady = !(request.Status == Models.Enums.OrderStatus.Active);

                await context.OrderActivities.AddAsync(new OrderActivity
                {
                    OrderId = query.Id,
                    EmployeeId = userId,
                    Description = "Update order's detail"
                });
                await context.SaveChangesAsync();
                return ServiceStatus.SuccessResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult();
            }
        }
        public async Task<ServiceStatus> UpdateOrder(Guid id, List<OrderedItemDTO> items, Guid userId)
        {
            try
            {
                var query = await context.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.Id == id)
                    .SingleOrDefaultAsync();
                if (query is null)
                    return ServiceStatus.ErrorResult<Order>(Constants.AppError.OrderIsNotFound);

                if (query.OrderItems.Any())
                {
                    context.RemoveRange(query.OrderItems);
                    await context.SaveChangesAsync();
                }

                int price = 0;
                foreach (var item in items)
                {
                    var selectedItem = await context.Items
                        .Where(i => i.Id == item.ItemId)
                        .SingleAsync();

                    query.OrderItems.Add(
                        new OrderItem
                        {
                            ItemId = item.ItemId,
                            Quantity = item.ItemId,
                            ProductName = selectedItem.Name,
                            PriceSnapshot = selectedItem.Price
                        }
                    );
                    price = price + (selectedItem.Price * item.Quantity);
                }
                query.Total = price;

                await context.SaveChangesAsync();

                await context.OrderActivities.AddAsync(new OrderActivity
                {
                    OrderId = query.Id,
                    EmployeeId = userId,
                    Description = "Update order's items"
                });
                await context.SaveChangesAsync();

                return ServiceStatus.SuccessResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult();
            }

        }
        public async Task<ServiceStatus> DeleteOrder(Guid id, Guid UserId)
        {
            try
            {
                var query = await context.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.Id == id)
                    .SingleOrDefaultAsync();

                if (query is null)
                    return ServiceStatus.ErrorResult<Order>(Constants.AppError.OrderIsNotFound);

                if (query.OrderItems.Any())
                {
                    context.RemoveRange(query.OrderItems);
                    await context.SaveChangesAsync();
                }

                context.Remove(query);
                await context.SaveChangesAsync();

                return ServiceStatus.SuccessResult();

            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult();
            }
        }

        public Task<ServiceStatus<Order>> GetOrder(int tableId)
        {
            throw new NotImplementedException();
        }
    }
}