namespace Contracts.Domains.Interfaces;

public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
{
    public DateTimeOffset CreateDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
}