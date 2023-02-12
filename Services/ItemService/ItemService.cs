using FoodOrderAPI.Models.DTOs;

namespace FoodOrderAPI.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly DataContext context;
        private readonly ILogger<ItemService> logger;
        public ItemService(DataContext context,
        ILogger<ItemService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<ServiceStatus<List<Item>>> GetItems(ItemQueryDTO param)
        {
            try
            {
                var query = await context.Items.AsNoTracking().ToListAsync();
                if (param.Type is not null) 
                    query = query.Where(i => i.Type == param.Type).ToList();

                return ServiceStatus.SuccessObjectResult<List<Item>>(query);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<List<Item>>();
            }
        }
        public async Task<ServiceStatus<Item>> GetItem(int id)
        {
            try
            {
                var result = await context.Items.FindAsync(id);
                if (result is null)
                    return ServiceStatus.ErrorResult<Item>(Constants.AppError.ItemIsNotFound);

                return ServiceStatus.SuccessObjectResult<Item>(result);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<Item>();
            }
        }
        public async Task<ServiceStatus> AddItem(ItemDTO request)
        {
            try
            {
                context.Items.Add(new Item
                {
                    Name = request.Name,
                    Price = request.Price,
                    IsReady = true
                });
                await context.SaveChangesAsync();
                return ServiceStatus.SuccessResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<Item>();
            }
        }
        public async Task<ServiceStatus> UpdateItem(int id, ItemDTO request)
        {
            try
            {
                var item = await context.FindAsync<Item>(id);
                if (item is null)
                    return ServiceStatus.ErrorResult<Item>(Constants.AppError.ItemIsNotFound);

                item.Name = request.Name;
                item.Price = request.Price;
                item.IsReady = request.IsReady;

                await context.SaveChangesAsync();
                return ServiceStatus.SuccessResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<Item>();
            }
        }
        public async Task<ServiceStatus> DeleteItem(int id)
        {
            try
            {
                var item = await context.FindAsync<Item>(id);
                if (item is null)
                    return ServiceStatus.ErrorResult<Item>(Constants.AppError.ItemIsNotFound);

                context.Remove(item);
                await context.SaveChangesAsync();
                return ServiceStatus.SuccessResult();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<Item>();
            }
        }
    }
}