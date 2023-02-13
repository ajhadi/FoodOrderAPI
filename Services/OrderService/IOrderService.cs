using FoodOrderAPI.Models.DTOs;

namespace FoodOrderAPI.Services.OrderService
{
    public interface IOrderService
    {
        /// <summary> Create new order </summary>
        Task<ServiceStatus> CreateOrder(OrderDTO request, Guid userId);
        /// <summary> Get single order by Id </summary>
        Task<ServiceStatus<Order>> GetOrder(Guid id);
        /// <summary> Get active order by Table Id </summary>
        Task<ServiceStatus<Order>> GetActiveOrder(int tableId);
        /// <summary> Get list of order </summary>
        Task<ServiceStatus<List<Order>>> GetOrders(OrderQueryDTO param, Guid userId);
        /// <summary> Get employee's order activities </summary>
        Task<ServiceStatus<List<Order>>> GetOrderActivities(Guid userId);
        /// <summary> Update order's detail </summary>
        Task<ServiceStatus> UpdateOrder(Guid id, OrderDetailDTO request, Guid userId);
        /// <summary> Update order's items </summary>
        Task<ServiceStatus> UpdateOrder(Guid id, List<OrderedItemDTO> items, Guid userId);
        /// <summary> Delete order </summary>
        Task<ServiceStatus> DeleteOrder(Guid id, Guid userId);
    }
}