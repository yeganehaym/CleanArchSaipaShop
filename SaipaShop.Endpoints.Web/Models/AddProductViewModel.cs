using SaipaShop.Application.CQRS.Commands;
using SaipaShop.Application.Dto.Products;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Endpoints.Web.Models;

public class AddProductViewModel
{
    public CreateOrUpdateProductCommand Command { get; set; }
    public Result<ProductDto> Result { get; set; }
}