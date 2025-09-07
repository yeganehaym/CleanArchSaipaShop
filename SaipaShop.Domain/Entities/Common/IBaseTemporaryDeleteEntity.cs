namespace SaipaShop.Domain.Entities.Common;

public interface IBaseTemporaryDeleteEntity
{
    DateTimeOffset? TemporaryDelete { get; set; }
}