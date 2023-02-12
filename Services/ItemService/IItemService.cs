using FoodOrderAPI.Models.DTOs;

namespace FoodOrderAPI.Services.ItemService
{
    public interface IItemService
    {
        /// <summary> Add new item </summary>
        Task<ServiceStatus> AddItem(ItemDTO request);
        /// <summary> Get list of item </summary>
        Task<ServiceStatus<List<Item>>> GetItems(ItemQueryDTO param);
        /// <summary> Get item by Id </summary>
        Task<ServiceStatus<Item>> GetItem(int id);
        /// <summary> Update item </summary>
        Task<ServiceStatus> UpdateItem(int id, ItemDTO request);
        /// <summary> Delete item </summary>
        Task<ServiceStatus> DeleteItem(int id);

    }
}