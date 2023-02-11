using FoodOrderAPI.Models.Params;
using FoodOrderAPI.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet]
    public async Task<ActionResult<List<Item>>> GetAllItems()
    {
        var result = await itemService.GetAllItems();
        if (result is null)
            return NotFound("Item not found.");
        return Ok(result);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<List<Item>>> GetItem(int id)
    {
        var result = await itemService.GetItemById(id);
        if (result is null)
            return NotFound("Sorry, but this item doesn't exist.");
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddItem(ItemParam request)
    {
        await itemService.AddItem(request);
        return Ok(request);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Item>> UpdateItem(int id, ItemParam request)
    {
        var result = await itemService.UpdateSingleItem(id, request);
        if (result is null)
            return NotFound("Sorry, but this item doesn't exist.");
        return Ok(result);

    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Item>> DeleteItem(int id)
    {
        var result = await itemService.DeleteSingleItem(id);
        if (result is null)
            return NotFound("Sorry, but this item doesn't exist.");
        return Ok(result);
    }
}
