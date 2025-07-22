using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Features.Items.CreateItem;
using Ambev.DeveloperEvaluation.Application.Items.CreateItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Items.GetItem;
using Ambev.DeveloperEvaluation.Application.Items.GetItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Items;

/// <summary>
/// Controller for managing Items operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ItemsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ItemsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ItemsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new item
    /// </summary>
    /// <param name="request">The item creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created item details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateItemResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateItemCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateItemResponse>
        {
            Success = true,
            Message = "Item created successfully",
            Data = _mapper.Map<CreateItemResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves all items
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All created items</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetItemResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllItems(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllItemsQuery(), cancellationToken);
        return Ok(_mapper.Map<IEnumerable<GetItemResponse>>(response));
    }
}
