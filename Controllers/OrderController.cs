using FoodOrderAPI.Extensions;
using FoodOrderAPI.Models.DTOs;
using FoodOrderAPI.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Constants;

namespace FoodOrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> logger;
    private readonly IOrderService orderService;

    public OrderController(ILogger<OrderController> logger,
        IOrderService orderService)
    {
        this.logger = logger;
        this.orderService = orderService;
    }

    [HttpPost, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    public async Task<ActionResult<List<Order>>> CreateOrder(OrderDTO request)
    {
        var result = await orderService.CreateOrder(request, User.GetUserId());
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }

    [HttpGet, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        var param = new OrderQueryDTO { };
        if (!(User.GetRole() == UserRole.Admin)) param.Status = Models.Enums.OrderStatus.Active;
        var result = await orderService.GetOrders(param, new Guid());
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }

    [HttpGet, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    [Route("{id}")]
    public async Task<ActionResult<List<Order>>> GetOrder(Guid id)
    {
        var result = await orderService.GetOrder(id);
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }
    
    [HttpGet, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    [Route("Active/{tableId}")]
    public async Task<ActionResult<List<Order>>> GetActiveOrder(int tableId)
    {
        var result = await orderService.GetActiveOrder(tableId);
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }

    [HttpGet, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    [Route("/Activity/Report")]
    public async Task<ActionResult<List<Order>>> GetActivityReport()
    {
        var result = await orderService.GetOrderActivities(User.GetUserId());
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }

    [HttpPut, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}")]
    [Route("{id}/Detail")]
    public async Task<ActionResult<List<Order>>> UpdateOrderDetail(Guid id, OrderDetailDTO request)
    {
        var result = await orderService.UpdateOrder(id, request, User.GetUserId());
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }
    
    [HttpPut, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    [Route("{id}/Items")]
    public async Task<ActionResult<List<Order>>> UpdateOrderItems(Guid id, List<OrderedItemDTO> items)
    {
        var result = await orderService.UpdateOrder(id, items, User.GetUserId());
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }
    
    [HttpDelete, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    [Route("{id}")]
    public async Task<ActionResult<List<Order>>> UpdateOrderItems(Guid id)
    {
        var result = await orderService.DeleteOrder(id, User.GetUserId());
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }


}
