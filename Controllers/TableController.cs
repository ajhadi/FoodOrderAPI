using FoodOrderAPI.Models.DTOs;
using FoodOrderAPI.Services.TokenService;
using FoodOrderAPI.Services.UserService;
using Microsoft.AspNetCore.Authorization;
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
    [HttpGet, Authorize(Roles = $"{UserRole.Admin}, {UserRole.Cashier}, {UserRole.Waiter}")]
    public async Task<ActionResult<List<TableDTO>>> GetTables()
    {
        var query = await context.Tables.Select(o => new TableDTO
        {
            Id = o.Id,
            Name = o.Name,
            IsReady = o.IsReady
        }).ToListAsync();
        return Ok(query);
    }
}