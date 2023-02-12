public static partial class Constants
{
    public static class AppError
    {
        public static Error DefaultErrorMessage => Error.Create(StatusCodes.Status500InternalServerError, 1000, "Something happens on the server");
        public static Error ItemIsNotFound => Error.Create(StatusCodes.Status404NotFound, 1001, "Item is not found");
        public static Error UserIsNotFound => Error.Create(StatusCodes.Status404NotFound, 1002, "User is not found");
        public static Error WrongPassword => Error.Create(StatusCodes.Status400BadRequest, 1003, "Password incorrect");
        public static Error OrderIsNotFound => Error.Create(StatusCodes.Status404NotFound, 1004, "Order is not found");
    }
}