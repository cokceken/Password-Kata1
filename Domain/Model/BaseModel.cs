namespace Password.Domain.Model
{
    public abstract class BaseModel<T>
    {
        public virtual T Id { get; set; }
    }
}
