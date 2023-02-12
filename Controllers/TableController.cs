using FoodOrderAPI.Models.DTOs;
using FoodOrderAPI.Services.TokenService;
using FoodOrderAPI.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using static Constants;

namespace FoodOrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController : ControllerBase
{
    private readonly DataContext context;

    public TableController(DataContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Get list of table
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<TableDTO>>> GetTables()
    {
        var query = await context.Tables.ToListAsync();
        return Ok(query);
    }

    /// <summary>
    /// Get active order on table
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}/ActiveOrder")]
    public async Task<ActionResult<List<TableDTO>>> GetTables(int id)
    {
        var query = await context.Orders.Where(o => (o.TableId == id) && (o.Status == Models.Enums.OrderStatus.Active))
        .SingleOrDefaultAsync();
        return Ok(query);
    }
}