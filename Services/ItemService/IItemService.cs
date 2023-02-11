using FoodOrderAPI.Models.Params;

namespace FoodOrderAPI.Services.ItemService
{
    public interface IItemService
    {
        Task<Item> AddItem(ItemParam request);
        Task<List<Item>> GetAllItems();
        Task<Item> GetItemById(int id);
        Task<Item> UpdateSingleItem(int id, ItemParam request);
        Task<Item> DeleteSingleItem(int id);

    }
}