using SaipaShop.Domain.Entities.Common;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Enums;

namespace SaipaShop.Application.RepositoryFilters;

public class UserListFilter:BaseListFilter<User>
{
    public int? Status { get; set; }
    public override IQueryable<User> SetFilterQuery(IQueryable<User> query)
    {
        if (IsSearchable)
        {
            query = query
                .Where(x => (x.FirstName + " " + x.LastName).Contains(SearchWord)
                || x.Username==SearchWord);
        }

        if (Status.HasValue)
        {
            query = query
                .Where(x => x.Status == (UserStatus)Status);
        }

        return query;
    }
}