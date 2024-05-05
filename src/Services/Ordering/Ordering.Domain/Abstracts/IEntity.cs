namespace Ordering.Domain.Abstracts;

public interface IEntity<T> : IEntity
{
    T Id { get; set; }
}

public interface IEntity
{
    public DateTime? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set;}

}
