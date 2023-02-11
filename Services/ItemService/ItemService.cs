using FoodOrderAPI.Models.Params;

namespace FoodOrderAPI.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly DataContext context;
        public ItemService(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<Item>> GetAllItems()
        {
            return await context.Items.ToListAsync();
        }
        public async Task<Item> GetItemById(int id)
        {
            var item = await context.Items.FindAsync(id);
            if (item is null)
                return null;

            return item;
        }
        public async Task<Item> AddItem(ItemParam request)
        {
            context.Items.Add(new Item {
                Name = request.Name,
                Price = request.Price
            });
            await context.SaveChangesAsync();
            return new Item();
        }
        public async Task<Item> UpdateSingleItem(int id, ItemParam request)
        {
            var item = await context.FindAsync<Item>(id);
            if (item is null)
                return null;

            item.Name = request.Name;
            item.Price = request.Price;

            await context.SaveChangesAsync();

            return item;
        }
        public async Task<Item> DeleteSingleItem(int id)
        {
            var item = await context.FindAsync<Item>(id);
            if (item is null)
                return null;

            context.Remove(item);
            await context.SaveChangesAsync();
            return item;
        }
    }
}