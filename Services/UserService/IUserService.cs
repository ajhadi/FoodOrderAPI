namespace FoodOrderAPI.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceStatus<User>> GetUserByUsername(string username);
    }
}