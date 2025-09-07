using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaipaShop.Application.CQRS.Commands;
using SaipaShop.Application.Dto.Products;
using SaipaShop.Endpoints.Web.Models;

namespace SaipaShop.Endpoints.Web.Controllers;

public class ProductController:Controller
{
    private IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(CreateOrUpdateProductCommand command)
    {
        var result = await _mediator.Send(command);

        var vm = new AddProductViewModel()
        {
            Command = command,
            Result = result
        };
        return View(vm);
    }
}