namespace NewsWebSite.Domain.Entities.Commons
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<long>
    {

    }
}
