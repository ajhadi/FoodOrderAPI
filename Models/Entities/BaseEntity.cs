namespace FoodOrderAPI.Models.Entities
{
    public interface IBaseEntity
    {
        DateTime CreatedDateUTC { get; set; }
        string CreatedBy { get; set; }
        bool IsDeleted { get; set; }
    }
    public abstract class BaseEntity : IBaseEntity
    {
        public DateTime CreatedDateUTC { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
    }
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
    public abstract class BaseEntity<T> : BaseEntity, IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}