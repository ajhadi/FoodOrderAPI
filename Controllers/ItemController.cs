using FoodOrderAPI.Extensions;
using FoodOrderAPI.Models.DTOs;
using FoodOrderAPI.Services.ItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Constants;

namespace FoodOrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> logger;
    private readonly IItemService itemService;

    public ItemController(ILogger<ItemController> logger,
        IItemService itemService)
    {
        this.logger = logger;
        this.itemService = itemService;
    }

    [HttpGet, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    public async Task<ActionResult<List<Item>>> GetItems([FromQuery]ItemQueryDTO param)
    {
        var userId = User.GetUserId();
        var role = User.GetRole();
        var result = await itemService.GetItems(param);
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }

    [HttpGet, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    [Route("{id}")]
    public async Task<ActionResult<List<Item>>> GetItem(int id)
    {
        var result = await itemService.GetItem(id);
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);
    }

    [HttpPost, Authorize(Roles = UserRole.Admin)]
    public async Task<IActionResult> AddItem(ItemDTO request)
    {
        var result = await itemService.AddItem(request);
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut, Authorize(Roles = UserRole.Admin)]
    [Route("{id}")]
    public async Task<ActionResult<Item>> UpdateItem(int id, ItemDTO request)
    {
        var result = await itemService.UpdateItem(id, request);
        if (!result.IsSuccess)
            throw new ApiException(result.Error);
        return Ok(result);

    }
    
    [HttpDelete, Authorize(Roles = UserRole.Admin)]
    [Route("{id}")]
    public async Task<ActionResult<Item>> DeleteItem(int id)
    {
        var result = await itemService.DeleteItem(id);
        if (!result.IsSuccess)
            return StatusCode(result.Error.HttpStatusCode, result.Error);
        return Ok(result);
    }
}
