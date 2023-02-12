namespace FoodOrderAPI.Services.UserService
{
    class UserService : IUserService
    {
        private readonly DataContext context;
        private readonly ILogger<UserService> logger;
        public UserService(DataContext context,
        ILogger<UserService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<ServiceStatus<User>> GetUserByUsername(string username)
        {
            try
            {
                var result = await context.Users.Where(u => u.Username == username).SingleOrDefaultAsync();
                if (result is null)
                    return ServiceStatus.ErrorResult<User>(Constants.AppError.UserIsNotFound);

                return ServiceStatus.SuccessObjectResult<User>(result);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<User>();
            }
        }
    }
}